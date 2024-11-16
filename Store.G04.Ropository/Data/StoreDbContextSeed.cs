using Store.G04.Core.Entities;
using Store.G04.Core.Entities.OrderEntities;
using Store.G04.Ropository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Ropository.Data
{
    public static class StoreDbContextSeed
    {
        public async static Task SeedAsync(StoreDbContext _context) 
        {
            if(_context.Brands.Count() == 0)
            {
                // Brand 
                // 1.Read Data From Json File

                var brandData = File.ReadAllText(@"..\Store.G04.Ropository\Data\DataSeed\brands.json");

                //2.Convert Json String To List 

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                //3. Seed Data To DB
                if (brands is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Types.Count() == 0) 
            {
                var typedata = File.ReadAllText(@"..\Store.G04.Ropository\Data\DataSeed\types.json");

                var types =  JsonSerializer.Deserialize<List<ProductType>>(typedata);

                if(types is not null && types.Count() > 0) 
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }
            if (_context.Products.Count() == 0)
            {
                var productdata = File.ReadAllText(@"..\Store.G04.Ropository\Data\DataSeed\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productdata);

                if(products is not null && products.Count() > 0) 
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }

            }
            if (_context.DeliveryMethods.Count() == 0)
            {
                var deliverydata = File.ReadAllText(@"..\Store.G04.Ropository\Data\DataSeed\delivery.json");

                var deliveryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverydata);

                if (deliveryMethod is not null && deliveryMethod.Count() > 0)
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethod);
                    await _context.SaveChangesAsync();
                }

            }
        }
    }
}
