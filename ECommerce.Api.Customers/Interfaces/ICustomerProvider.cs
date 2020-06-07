using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSucess, IEnumerable<Models.Customer> Customers, string ErrMessage)> GetCustomerAsycn();
        Task<(bool IsSucess, Models.Customer Customer, string ErrMessage)> GetCustomerAsycnById(int id);

    }
}
