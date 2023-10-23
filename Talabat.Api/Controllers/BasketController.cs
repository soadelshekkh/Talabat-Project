using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Api.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Reposatiries;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepositary basketRepositary;
        private readonly IMapper mapper;

        public BasketController(IBasketRepositary basketRepositary,IMapper mapper)
        {
            this.basketRepositary = basketRepositary;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<customerBasket>> GetCustomerBasket(string id)
        {
            var basket = await basketRepositary.GetCustomerBasket(id);
            return Ok( basket ?? new customerBasket(id));
        }
        [HttpPost]
        public  async Task<ActionResult<customerBasket>> Updatebasket(CustomerBasketDTO basket)
        {
            if(basket == null) 
                return new customerBasket("idGuide");
            var customerBasket = mapper.Map<CustomerBasketDTO,customerBasket>(basket); 
            var result = await basketRepositary.UpdateBasket(customerBasket);
            return Ok(result ?? new customerBasket("id guide"));
        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await basketRepositary.DeleteBasket(id);
        }
    }
}
