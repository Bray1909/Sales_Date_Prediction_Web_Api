using Application.DTOs;
using Application.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly IConfiguration _config;

        public ShipperRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<ShipperDto>> GetShippersAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var query = "SELECT shipperid, companyname FROM Sales.Shippers";

            var shippers = await connection.QueryAsync<ShipperDto>(query);

            return shippers.AsList();
        }
    }
}
