using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        //[ForeignKey("Productbrand")]
        public int ProductBrandId { get; set; }
        public ProductBrand Productbrand { get; set; }
        //[ForeignKey("productType")]
        public int ProductTypeId { get; set; }
        public ProductType productType { get; set; }
    }
}
