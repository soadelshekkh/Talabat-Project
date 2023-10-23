using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Api.DTOS;
using Talabat.Api.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.services;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderInfo)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await orderService.CreateOrderAsync(Email, orderInfo.BasketId, orderInfo.DeliveryMethodId, orderInfo.ShippingAddress);
            if (Order == null) return BadRequest(new ApiResponse(400));
            return Ok(Order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await orderService.GetOrdersForUserAsync(email);
            var mappedOrder = mapper.Map<IReadOnlyList< Order>,IReadOnlyList< OrderToReturnDto>>(Orders);
            return Ok(mappedOrder);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.GetOrderByIdForUserAsync(email, id);
            if (order == null) return BadRequest(new ApiResponse(400));
            var mappedOrder = mapper.Map<Order,OrderToReturnDto>(order);
            return Ok(mappedOrder);
        }
        [HttpGet("delivaryMethods")]
        public async Task<ActionResult<IReadOnlyList<Delivarymethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}

