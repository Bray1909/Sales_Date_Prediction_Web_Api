using Application.Dal;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.API.Middleware;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ExistsProduct _existsProduct;
        public OrderController(OrderService orderService, ExistsProduct existsProduct)
        {
            _orderService = orderService;
            _existsProduct = existsProduct;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);

                if (orders == null || orders.Count == 0)
                {
                    return NotFound($"No orders found for customer with ID {customerId}.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewOrder([FromBody] NewOrderRequest newOrderRequest)
        {
            if (newOrderRequest == null)
            {
                return BadRequest("The request does not contain valid data.");
            }

            foreach (var detail in newOrderRequest.OrderDetails)
            {
                bool exists = await _existsProduct.ExistsAsync(detail.ProductId);
                if (!exists)
                {
                    return BadRequest($"The product with ID {detail.ProductId} does not exist.");
                }
            }

            try
            {
                var orderId = await _orderService.AddNewOrderAsync(newOrderRequest);
                return CreatedAtAction(nameof(AddNewOrder), new { id = orderId }, new
                {
                    message = $"The order was created successfully with the ID {orderId}.",
                    orderId = orderId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding new order: {ex.Message}");
            }
        }
    }
}
