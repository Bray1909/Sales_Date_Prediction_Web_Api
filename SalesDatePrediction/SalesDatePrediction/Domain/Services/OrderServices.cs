using Application.Dal;
using Application.DTOs;
using Application.Interfaces;

namespace Domain.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);

            if (orders == null || orders.Count == 0)
            {
                return new List<OrderDto>();
            }

            return orders;
        }

        public async Task<int> AddNewOrderAsync(NewOrderRequest newOrderRequest)
        {
            try
            {
                var orderId = await _orderRepository.AddNewOrderAsync(newOrderRequest);

                return orderId;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al agregar la nueva orden", ex);
            }
        }

    }
}
