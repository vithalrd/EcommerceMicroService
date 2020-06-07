using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;
        public CustomersController(ICustomerProvider productProvider)
        {
            this.customerProvider = productProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerAsync()
        {
            var result = await customerProvider.GetCustomerAsycn();
            if (result.IsSucess)
                return Ok(result.Customers);

            return NotFound();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customerProvider.GetCustomerAsycnById(id);
            if (result.IsSucess)
                return Ok(result.Customer);

            return NotFound();

        }
    }
}
