using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> GetEmployeesAsync();
    }
}
