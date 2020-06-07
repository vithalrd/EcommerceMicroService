using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderssController : ControllerBase
    {
        private readonly IOrdersProvider orderProvider;
        public OrderssController(IOrdersProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }
       

        [HttpGet("{customerid}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await orderProvider.GetOrdersAsycnById(customerId);
            if (result.IsSucess)
                return Ok(result.Orders);

            return NotFound();

        }
    }
}
