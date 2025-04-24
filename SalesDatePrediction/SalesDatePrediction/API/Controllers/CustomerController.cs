using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("predictions")]
        public async Task<IActionResult> GetCustomersWithPredictedOrders()
        {
            try
            {
                var predictions = await _customerService.GetCustomersWithPredictedNextOrderAsync();

                if (predictions == null || !predictions.Any())
                    return NotFound("No prediction data available.");

                return Ok(predictions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}
