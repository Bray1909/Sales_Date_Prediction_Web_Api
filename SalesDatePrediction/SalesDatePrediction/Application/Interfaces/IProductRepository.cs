using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllProductsAsync();
    }
}
