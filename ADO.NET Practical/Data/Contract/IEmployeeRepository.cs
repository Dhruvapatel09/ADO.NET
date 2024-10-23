using ADO.NET_Practical.Dtos;
using ADO.NET_Practical.Models;

namespace ADO.NET_Practical.Data.Contract
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        bool AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int employeeId);
   
    }
}
