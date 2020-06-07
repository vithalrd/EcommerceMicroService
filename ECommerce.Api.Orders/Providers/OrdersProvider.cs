using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDBContext dbContext;
        private readonly ILogger<Db.Order> logger;
        private readonly IMapper mapper;
        public OrdersProvider(OrdersDBContext dbContext, ILogger<Db.Order> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Total = 8800,
                    Items = new List<Db.OrderItem>
                    {
                        new OrderItem
                        {
                            Id = 1, OrderId =1, ProductId =1, Quantity = 10, UnitPrice = 2300
                        },
                        new OrderItem
                        {
                            Id = 2, OrderId =1, ProductId =2, Quantity = 5, UnitPrice = 500
                        },
                        new OrderItem
                        {
                            Id = 3, OrderId =1, ProductId =3, Quantity = 6, UnitPrice = 6000
                        }
                    }
                });
                dbContext.Orders.Add(new Db.Order
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Total = 24000,
                    Items = new List<Db.OrderItem>
                    {
                        new OrderItem
                        {
                            Id = 4, OrderId =2, ProductId =1, Quantity = 10, UnitPrice = 5500
                        },
                        new OrderItem
                        {
                            Id = 5, OrderId =2, ProductId =2, Quantity = 15, UnitPrice = 500
                        },
                        new OrderItem
                        {
                            Id = 6, OrderId =2, ProductId =3, Quantity = 16, UnitPrice = 18000
                        }
                    }
                });



                dbContext.SaveChanges();
            }
        }

      
        public async Task<(bool IsSucess, IEnumerable<Models.Order> Orders, string ErrMessage)> GetOrdersAsycnById(int customerId)
        { 
            try
            {
                var order = await dbContext.Orders.Where(p => p.CustomerId == customerId).ToListAsync();
                if (order != null)
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(order);
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
