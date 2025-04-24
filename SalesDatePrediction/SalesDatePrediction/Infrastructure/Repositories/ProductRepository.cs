using Application.DTOs;
using Application.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _config;

        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    productid AS ProductId,
                    productname AS ProductName
                FROM [Production].[Products]
            ";

            var products = await connection.QueryAsync<ProductDto>(query);
            return products.ToList();
        }
    }
}
