using Application.DTOs;

namespace Application.Interfaces
{
    public interface IShipperRepository
    {
        Task<List<ShipperDto>> GetShippersAsync();
    }
}
