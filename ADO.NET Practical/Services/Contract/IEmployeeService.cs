using ADO.NET_Practical.Dtos;
using ADO.NET_Practical.Models;

namespace ADO.NET_Practical.Services.Contract
{
    public interface IEmployeeService
    {
        ServiceResponse<List<EmployeeDto>> GetAllEmployees();
        ServiceResponse<EmployeeDto> GetEmployeeById(int employeeId);
        ServiceResponse<string> AddEmployee(AddEmployeeDto employeeDto);
        ServiceResponse<string> UpdateEmployee(EmployeeDto employeeDto);
        ServiceResponse<string> DeleteEmployee(int employeeId);
    }
}
