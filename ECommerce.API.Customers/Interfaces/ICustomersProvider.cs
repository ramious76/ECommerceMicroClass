using ECommerce.API.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, CustomerDto Customer, string ErrorMessage)> GetCustomerAsync(int id);
        Task<(bool IsSuccess, IEnumerable<CustomerDto> Customers, string ErrorMessage)> GetCustomersAsync();
    }
}