using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.spcifications
{
    public class ProductSpecCount : BaseSpecificaton<Product>
    {
        public ProductSpecCount(productSpecififcationPrams Prams)
            :base(p=> (string.IsNullOrEmpty(Prams.search) || p.Name.ToLower().Contains(Prams.search)) && 
                      (!Prams.TypeId.HasValue|| Prams.TypeId.Value == p.ProductTypeId)&&
                      (!Prams.BrandId.HasValue|| Prams.BrandId.Value == p.ProductBrandId))
        {

        }
    }
}
