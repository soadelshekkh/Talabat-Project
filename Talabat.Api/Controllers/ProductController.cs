using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.Api.DTOS;
using Talabat.Api.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Reposatiries;
using Talabat.Core.spcifications;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepositary<Product> product;
        private readonly IGenericRepositary<ProductBrand> productBrand;
        private readonly IGenericRepositary<ProductType> productType;
        private readonly IMapper mapper;
        public ProductController(IGenericRepositary<Product> product,IMapper mapper, IGenericRepositary<ProductBrand> productBrand
            , IGenericRepositary<ProductType> productType)
        {
            this.product = product;
            this.productBrand = productBrand;
            this.productType = productType;
            this.mapper = mapper;
        }
        //[Cached(600)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery]productSpecififcationPrams Prams)
        {
            var spec = new ProductWithBrandAndTpyeSpecification(Prams);  
            var result = await product.GetAllSpec(spec);
            //var result = await product.GetAll();
            //return Ok(mapper.Map<IEnumerable< Product>,IEnumerable <ProductDTO>>(result));
            var Data = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(result);
            var SpecWithFilter = new ProductSpecCount(Prams);
            var count = await product.GetCount(SpecWithFilter);
            return Ok(new Pagination<ProductDTO>(Prams.pageSize,Prams.PageIndex,count,Data));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndTpyeSpecification(id);
            var result = await product.GetByIdSpec(spec);
            //var result = await product.GetById(id);
            return Ok(mapper.Map<Product,ProductDTO>(result));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var result = await productBrand.GetAll();
            return Ok(result);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var result = await productType.GetAll();
            return Ok(result);
        }
    }
    
}
