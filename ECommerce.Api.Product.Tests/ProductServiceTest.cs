using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profile;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace ECommerce.Api.Product.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase(nameof(GetProductReturnsAllProducts)).Options;
            var dbContext = new ProductDBContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductProvider(dbContext, null, mapper);

            var result = await productProvider.GetProductAsycn();
            Assert.True(result.IsSucess);
            Assert.True(result.Products.Any());
        }

        [Fact]
        public async Task GetProductReturnsProductsByValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase(nameof(GetProductReturnsAllProducts)).Options;
            var dbContext = new ProductDBContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductProvider(dbContext, null, mapper);

            var result = await productProvider.GetProductAsycnById(1);
            Assert.True(result.IsSucess);
            Assert.True(result.Product.Id == 1);
        }

        [Fact]
        public async Task GetProductReturnsProductsByInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDBContext>().UseInMemoryDatabase(nameof(GetProductReturnsAllProducts)).Options;
            var dbContext = new ProductDBContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductProvider(dbContext, null, mapper);

            var result = await productProvider.GetProductAsycnById(-1);
            Assert.False(result.IsSucess);
        }

        private void CreateProducts(ProductDBContext dbContext)
        {
            for(int i= 1; i<= 5; i++)
            {
                dbContext.Products.Add(new Products.Db.Product
                {
                    Id = i,
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14),
                    Name = Guid.NewGuid().ToString()
                });
            }
            dbContext.SaveChanges();
        }
    }
}
