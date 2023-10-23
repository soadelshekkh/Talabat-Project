﻿namespace Talabat.Api.DTOS
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductBrandId { get; set; }
        public string Productbrand { get; set; }
        public int ProductTypeId { get; set; }
        public string productType { get; set; }
    }
}
