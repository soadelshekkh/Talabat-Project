using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.spcifications
{
    public class ProductWithBrandAndTpyeSpecification : BaseSpecificaton<Product>
    {
        public ProductWithBrandAndTpyeSpecification(productSpecififcationPrams Prams): base
                 (p=> 
                 (string.IsNullOrEmpty(Prams.search) || p.Name.ToLower().Contains(Prams.search))&&
                 (!Prams.TypeId.HasValue|| Prams.TypeId.Value == p.ProductTypeId) &&
                 (!Prams.BrandId.HasValue|| Prams.BrandId.Value == p.ProductBrandId)
                 )
        {
            Includes.Add(p => p.productType);
            Includes.Add(p => p.Productbrand);
            if (!string.IsNullOrEmpty(Prams.sort))
            {
                switch (Prams.sort)
                {
                    case "OrderByPrice":
                        GetorderByExperresion( p => p.Price);
                        break;
                    case "OrderByDescPrice":
                        GetorderByExperresiondesc( p => p.Price);
                        break;
                    case "OrderBy":
                        GetorderByExperresion( p => p.Name);
                        break;
                    case "OrderByDesc":
                        GetorderByExperresiondesc( p => p.Name);
                        break;
                    default:
                        break;
                }
            }
            //int TakenNumber = (100/Prams.pageSize);
            ApplyPagination((Prams.PageIndex - 1) * Prams.pageSize, Prams.pageSize);
        }
        public ProductWithBrandAndTpyeSpecification(int id):base(p => p.Id == id)
        {
            Includes.Add(p => p.productType);
            Includes.Add(p => p.Productbrand);
        }
    }
}
