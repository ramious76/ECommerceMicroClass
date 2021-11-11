using ECommerce.API.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderDto> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}