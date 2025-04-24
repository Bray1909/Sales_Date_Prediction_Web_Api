using Application.Interfaces;
using Application.DTOs;
using Dapper;
using Microsoft.Data.SqlClient;
using Application.Dal;

namespace Infrastructure.Repositories
    {
        public class OrderRepository : IOrderRepository
        {
            private readonly IConfiguration _config;

            public OrderRepository(IConfiguration config)
            {
                _config = config;
            }

            public async Task<List<OrderDto>> GetOrdersByCustomerIdAsync(int customerId)
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.OpenAsync();

                var query = @"
                SELECT 
                    o.orderid, 
                    o.requireddate, 
                    o.shippeddate, 
                    o.shipname, 
                    o.shipaddress, 
                    o.shipcity
                FROM 
                    Sales.Orders o
                WHERE 
                    o.custid = @CustomerId";

                var orders = await connection.QueryAsync<OrderDto>(query, new { CustomerId = customerId });

                return orders.AsList();
            }

        public async Task<int> AddNewOrderAsync(NewOrderRequest newOrderRequest)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                var insertOrderQuery = @"
                INSERT INTO Sales.Orders (Empid, Shipperid, Shipname, Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry)
                VALUES (@Empid, @Shipperid, @Shipname, @Shipaddress, @Shipcity, @Orderdate, @Requireddate, @Shippeddate, @Freight, @Shipcountry);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                var orderId = await connection.QuerySingleAsync<int>(insertOrderQuery, new
                {
                    Empid = newOrderRequest.EmpId,
                    Shipperid = newOrderRequest.ShipperId,
                    Shipname = newOrderRequest.ShipName,
                    Shipaddress = newOrderRequest.ShipAddress,
                    Shipcity = newOrderRequest.ShipCity,
                    Orderdate = newOrderRequest.OrderDate,
                    Requireddate = newOrderRequest.RequiredDate,
                    Shippeddate = newOrderRequest.ShippedDate,
                    Freight = newOrderRequest.Freight,
                    Shipcountry = newOrderRequest.ShipCountry
                }, transaction);

                var insertOrderDetailsQuery = @"
                INSERT INTO Sales.OrderDetails (Orderid, Productid, Unitprice, Qty, Discount)
                VALUES (@Orderid, @Productid, @Unitprice, @Qty, @Discount);";

                foreach (var detail in newOrderRequest.OrderDetails)
                {
                    await connection.ExecuteAsync(insertOrderDetailsQuery, new
                    {
                        Orderid = orderId,
                        Productid = detail.ProductId,
                        Unitprice = detail.UnitPrice,
                        Qty = detail.Quantity,
                        Discount = detail.Discount
                    }, transaction);
                }

                transaction.Commit();

                return orderId;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}

