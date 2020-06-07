using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductProvider productProvider;
        public ProductsController(IProductProvider productProvider)
        {
            this.productProvider = productProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await productProvider.GetProductAsycn();
            if (result.IsSucess)
                return Ok(result.Products);

            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productProvider.GetProductAsycnById(id);
            if (result.IsSucess)
                return Ok(result.Product);

            return NotFound();

        }
    }
}
