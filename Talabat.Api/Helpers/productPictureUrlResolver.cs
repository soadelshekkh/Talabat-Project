using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.Api.DTOS;
using Talabat.Core.Entities;

namespace Talabat.Api.Helpers
{
    public class productPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        public IConfiguration Configuration { get; }
        public productPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            return $"{Configuration["BaseApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}
