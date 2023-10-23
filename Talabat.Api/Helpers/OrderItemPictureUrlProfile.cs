using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Api.DTOS;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.Helpers
{
    public class OrderItemPictureUrlProfile : IValueResolver<OrderItem, OrdereItemDto, string>
    {
        public IConfiguration Configuration { get; }
        public OrderItemPictureUrlProfile (IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Resolve(OrderItem source, OrdereItemDto destination, string destMember, ResolutionContext context)
        {
            if(! string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
            return $"{Configuration["BaseApiUrl"]}{source.ProductItemOrdered.PictureUrl}";
            return null;
        }
    }
}
