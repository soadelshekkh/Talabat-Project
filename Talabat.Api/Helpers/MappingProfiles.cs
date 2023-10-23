using AutoMapper;
using Talabat.Api.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
            .ForMember(d => d.Productbrand, o => o.MapFrom(s => s.Productbrand.Name))
            .ForMember(d=>d.productType, o=>o.MapFrom(s=>s.productType.Name))
            .ForMember(d=> d.PictureUrl, o=>o.MapFrom<productPictureUrlResolver>());
            CreateMap<AdrressDTO, Address>().ReverseMap();
            CreateMap<CustomerBasketDTO, customerBasket>().ReverseMap();
            CreateMap<BasketItemDTO, BasketItem>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember( o => o.delivarymethod, o =>o.MapFrom(s => s.delivarymethod.ShortName))
                .ForMember(o => o.DeliverMethodCost , o => o.MapFrom(s => s.delivarymethod.Cost));
            CreateMap<OrderItem, OrdereItemDto>()
                .ForMember(d => d.ProductName, O => O.MapFrom( o => o.ProductItemOrdered.ProductName))
                .ForMember(d => d.ProductId , O => O.MapFrom( o => o.ProductItemOrdered.ProductId))
                .ForMember(d => d.PictureUrl, O => O.MapFrom(o => o.ProductItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlProfile>());
        }
    }
}
