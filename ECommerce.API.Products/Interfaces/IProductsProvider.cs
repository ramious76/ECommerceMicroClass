using ECommerce.API.Products.Db;
using ECommerce.API.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, ProductDto Product, string ErrorMessage)> GetProductAsync(int id);
        Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync();
    }
}