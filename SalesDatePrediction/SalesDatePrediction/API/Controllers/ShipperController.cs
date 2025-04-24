using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShipperController : ControllerBase
    {
        private readonly ShipperService _shipperService;
        public ShipperController(ShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShippers()
        {
            try
            {
                var shippers = await _shipperService.GetAllShippersAsync();

                if (shippers == null || !shippers.Any())
                {
                    return NotFound("No shippers found.");
                }

                return Ok(shippers);
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
