using ECommerce.API.Customers.Db;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.API.Customers.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.API.Customers.Interfaces;

namespace ECommerce.API.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomerDbContext context;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomerDbContext context, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!context.Customers.Any())
            {
                context.Customers.Add(new Customer() { Id = 1, Name = "A, B", Adddress = "123 A ST" });
                context.Customers.Add(new Customer() { Id = 2, Name = "C, D", Adddress = "234 B ST" });
                context.Customers.Add(new Customer() { Id = 3, Name = "E, F", Adddress = "567 C ST" });
                context.Customers.Add(new Customer() { Id = 4, Name = "H, I", Adddress = "890 D ST" });
                context.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerDto> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var Customers = await context.Customers.ToListAsync();
                if (Customers != null && Customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(Customers);
                    return (true, result, null);
                }
                return (false, null, "Noot found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, CustomerDto Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var Customer = await context.Customers.FirstOrDefaultAsync(p => p.Id == id);
                if (Customer != null)
                {
                    var result = mapper.Map<Customer, CustomerDto>(Customer);
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
