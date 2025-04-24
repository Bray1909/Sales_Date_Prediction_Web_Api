using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class ShipperService
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<List<ShipperDto>> GetAllShippersAsync()
        {
            var shippers = await _shipperRepository.GetShippersAsync();

            var shipperDtos = shippers.Select(s => new ShipperDto
            {
                ShipperId = s.ShipperId,
                CompanyName = s.CompanyName
            }).ToList();

            return shipperDtos;
        }

    }
}
