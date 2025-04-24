using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();


            return employees;
        }

    }
}
