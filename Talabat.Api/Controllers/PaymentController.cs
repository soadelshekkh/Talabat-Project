using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.IO;
using System;
using System.Threading.Tasks;
using Talabat.Api.DTOS;
using Talabat.Api.Errors;
using Talabat.Core.Entities;
using Talabat.Core.services;
using Talabat.Core.Entities.Order_Aggregate;
using Microsoft.Extensions.Logging;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Ipayment payment;
        private readonly ILogger<PaymentController> logger;
        private readonly string WhSecret = "whsec_07b1f7cda3c6902c35b17a89cb4477a9b9d53eb5de41c9963bb1c32f4c3c99be";
        public PaymentController(IMapper mapper, Ipayment payment, ILogger<PaymentController> logger)
        {
            this.mapper = mapper;
            this.payment = payment;
            this.logger = logger;
        }
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<customerBasket> > CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await payment.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400 , "a problem with your basket"));
            return Ok(basket);
        }
        [HttpPost("webhook")]
        public async Task<ActionResult> Webhooks()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], WhSecret);
                PaymentIntent intent;
                Order order;
                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        intent = (PaymentIntent) stripeEvent.Data.Object;
                        order = await payment.updatePaymentIntentSucceedOrFailed(intent.Id, true);
                        logger.LogInformation($"payment succeeded order id = {order.Id}");
                        break;
                    case Events.PaymentIntentPaymentFailed :
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        order = await payment.updatePaymentIntentSucceedOrFailed(intent.Id, false);
                        logger.LogInformation($"payment Failed order id = {order.Id}");
                        break;
                }
                return new EmptyResult();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
