using Dapper;
using Microsoft.Data.SqlClient;

namespace SalesDatePrediction.API.Middleware
{
    public class ExistsProduct
    {
        private readonly IConfiguration _config;

        public ExistsProduct(IConfiguration config)
        {
            _config = config;
        }
        public async Task<bool> ExistsAsync(int productId)
        {

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var query = @"
                SELECT COUNT(1)
                FROM [Production].[Products]
                WHERE productid = @ProductId
            ";

            var count = await connection.ExecuteScalarAsync<int>(query, new { ProductId = productId });
            return count > 0;
        }


    }
}
