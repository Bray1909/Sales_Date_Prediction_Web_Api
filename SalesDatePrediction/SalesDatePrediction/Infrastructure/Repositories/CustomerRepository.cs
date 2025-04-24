using Application.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _config;

        public CustomerRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<CustomerOrderPredictionDto>> GetCustomersWithOrdersAsync()
        {
            var query = @"
                WITH OrderDifferences AS (
                         SELECT 
                             o.custid,
                             DATEDIFF(DAY, LAG(o.orderdate) OVER (PARTITION BY o.custid ORDER BY o.orderdate), o.orderdate) AS DaysBetweenOrders
                         FROM 
                             Sales.Orders o
                         WHERE 
                             o.custid IS NOT NULL
                     ),
                     AverageDays AS (
                         SELECT 
                             custid,
                             AVG(DaysBetweenOrders) AS AvgDaysBetweenOrders
                         FROM 
                             OrderDifferences
                         WHERE 
                             DaysBetweenOrders IS NOT NULL
                         GROUP BY 
                             custid
                     )
                     SELECT
                         c.custid,
                         c.companyname AS CustomerName,
                         MAX(o.orderdate) AS LastOrderDate,
                         DATEADD(DAY, a.AvgDaysBetweenOrders, MAX(o.orderdate)) AS NextPredictedOrder
                     FROM 
                         Sales.Orders o
                     JOIN 
                         Sales.Customers c ON o.custid = c.custid
                     JOIN 
                         AverageDays a ON o.custid = a.custid
                     GROUP BY 
                         c.companyname, a.AvgDaysBetweenOrders, c.custid
                     ORDER BY 
                         c.companyname;
                    ";

            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            var result = await connection.QueryAsync<CustomerOrderPredictionDto>(query);
            return result.ToList();
        }
    }
}
