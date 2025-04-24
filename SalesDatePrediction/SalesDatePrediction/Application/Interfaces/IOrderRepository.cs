using Application.Dal;
using Application.DTOs;


namespace Application.Interfaces
{
    public interface IOrderRepository
    {
        
        Task<List<OrderDto>> GetOrdersByCustomerIdAsync(int customerId);
        Task<int> AddNewOrderAsync(NewOrderRequest newOrderRequest);
        
    }
}
