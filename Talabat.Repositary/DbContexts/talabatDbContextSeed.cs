using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositary.DbContexts
{
    public class talabatDbContextSeed
    {
        public static async Task  seedAsync(TalabatContext context, ILoggerFactory loggerFactory)
        {
            if (!context.productBrands.Any()) {
                try
                {
                    var brandsData = File.ReadAllText("../Talabat.Repositary/Data/Dataseed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<talabatDbContextSeed>();
                    logger.LogError(ex, ex.Message);
                }
            }
            if (!context.productTypes.Any())
            {
                try
                {
                    var productType = File.ReadAllText("../Talabat.Repositary/Data/Dataseed/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(productType);
                    foreach (var Type in Types)
                    {
                        context.Set<ProductType>().Add(Type);
                    }
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<talabatDbContextSeed>();
                    logger.LogError(ex, ex.Message);
                }
            }
            if (!context.products.Any())
            {
                try
                {
                    var productsSeed = File.ReadAllText("../Talabat.Repositary/Data/Dataseed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsSeed);
                    foreach (var product in products)
                    {
                        context.Set<Product>().Add(product);
                    }
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<talabatDbContextSeed>();
                    logger.LogError(ex, ex.Message);
                }
            }
            if (!context.DelivaryMethods.Any())
            {
                try
                {
                    var DelivaryMethodSeed = File.ReadAllText("../Talabat.Repositary/Data/Dataseed/delivery.json");
                    var deliveryMethod = JsonSerializer.Deserialize<List<Delivarymethod>>(DelivaryMethodSeed);
                    foreach (var delivarymethod in deliveryMethod)
                    {
                        context.Set<Delivarymethod>().Add(delivarymethod);
                    }
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<talabatDbContextSeed>();
                    logger.LogError(ex, ex.Message);
                }
            }
        }
    }
}
