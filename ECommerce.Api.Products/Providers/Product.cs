using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly ProductDBContext dbContext;
        private readonly ILogger<Db.Product> logger;
        private readonly IMapper mapper;
        public ProductProvider(ProductDBContext dbContext, ILogger<Db.Product> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product { Id = 1, Inventory = 100, Name = "Keyboard", Price = 2300 });
                dbContext.Products.Add(new Db.Product { Id = 2, Inventory = 101, Name = "Mouse", Price = 500 });
                dbContext.Products.Add(new Db.Product { Id = 3, Inventory = 101 ,Name = "Monitor", Price = 6000 });
                dbContext.Products.Add(new Db.Product { Id = 4, Inventory = 100, Name = "CPU", Price = 8500 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSucess, IEnumerable<Models.Product> Products, string ErrMessage)> GetProductAsycn()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if(products !=null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>,IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSucess, Models.Product Product, string ErrMessage)> GetProductAsycnById(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
