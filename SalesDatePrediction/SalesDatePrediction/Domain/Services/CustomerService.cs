using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerOrderPredictionDto>> GetCustomersWithPredictedNextOrderAsync()
        {
            var predictions = await _customerRepository.GetCustomersWithOrdersAsync();

            return predictions;
        }

    }
}
