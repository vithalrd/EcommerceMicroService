using ECommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var orderResult = await orderService.GetOrderAsync(customerId);
            var productResult = await productService.GetProductAsync();
            var customerResult = await customerService.GetCustomerAsync(customerId);

            if (orderResult.IsSuccess)
            {
                foreach(var order in orderResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ?
                            productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product Information is not available";
                    }
                }
                var result = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer :
                    new { Name = "Customer Information is not available" },
                    Orders = orderResult.Orders
                };

                return (true, result);
            }
            return (false, null);
        }
    }
}
