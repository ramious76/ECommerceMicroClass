using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext context;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext context, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!context.Products.Any())
            {
                context.Products.Add(new Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                context.Products.Add(new Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 });
                context.Products.Add(new Product() { Id = 3, Name = "Monitor", Price = 100, Inventory = 100 });
                context.Products.Add(new Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 2000 });
                context.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await context.Products.ToListAsync();
                if(products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
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

        public async Task<(bool IsSuccess, ProductDto Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Product, ProductDto>(product);
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


    }
}
