﻿using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerOrderPredictionDto>> GetCustomersWithOrdersAsync();
    }
}
