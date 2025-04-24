using Application.DTOs;
using Application.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _config;

        public EmployeeRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    Empid,
                    CONCAT(FirstName, ' ', LastName) AS FullName
                FROM 
                    [HR].[Employees]";

            var employees = await connection.QueryAsync<EmployeeDto>(query);
            return employees.AsList();
        }
    }
}
