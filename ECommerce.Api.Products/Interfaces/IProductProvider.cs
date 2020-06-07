using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductProvider
    {
        Task<(bool IsSucess, IEnumerable<Models.Product> Products, string ErrMessage)> GetProductAsycn();
        Task<(bool IsSucess, Models.Product Product, string ErrMessage)> GetProductAsycnById(int id);

    }
}
