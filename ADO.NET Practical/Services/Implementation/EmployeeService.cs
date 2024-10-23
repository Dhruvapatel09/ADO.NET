using ADO.NET_Practical.Data.Contract;
using ADO.NET_Practical.Dtos;
using ADO.NET_Practical.Models;
using ADO.NET_Practical.Services.Contract;

namespace ADO.NET_Practical.Services.Implementation
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ServiceResponse<List<EmployeeDto>> GetAllEmployees()
        {
            var response = new ServiceResponse<List<EmployeeDto>>();

            var employees = _employeeRepository.GetAllEmployees();

            if (employees.Any())
            {

                List<EmployeeDto> employeeDto = new List<EmployeeDto>();
                foreach (var employee in employees)
                {
                    employeeDto.Add(new EmployeeDto()
                    {

                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.EmployeeName,
                        EmployeeDesignation = employee.EmployeeDesignation,
                        EmployeeDOB = employee.EmployeeDOB,

                    });
                }

                response.Success = true;
                response.Data = employeeDto;
            }
            else
            {
                response.Success = false;
                response.Message = "No employee found!";
            }

            return response;
        }
        public ServiceResponse<EmployeeDto> GetEmployeeById(int employeeId)
        {
            var response = new ServiceResponse<EmployeeDto>();

            var employee = _employeeRepository.GetEmployeeById(employeeId);

            if (employee != null)
            {

                var employeeDto = new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    EmployeeName = employee.EmployeeName,
                    EmployeeDesignation = employee.EmployeeDesignation,
                    EmployeeDOB = employee.EmployeeDOB,
                };

                response.Data = employeeDto;
                response.Success = true;

            }
            else
            {
                response.Success = false;
                response.Message = "Employee not found!";
            }

            return response;
        }
        public ServiceResponse<string> AddEmployee (AddEmployeeDto employeeDto)
        {
            var response = new ServiceResponse<string>();

            var employeeModel = new Employee
            {
                EmployeeName = employeeDto.EmployeeName,
                EmployeeDesignation = employeeDto.EmployeeDesignation,
                EmployeeDOB = employeeDto.EmployeeDOB,
            };

            var result = _employeeRepository.AddEmployee(employeeModel);
            if (result)
            {
                response.Success = true;
                response.Message = "Employee added successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
            }
            return response;
        }
        public ServiceResponse<string> UpdateEmployee(EmployeeDto employeeDto)
        {
            var response = new ServiceResponse<string>();

            var employeeModel = new Employee
            {
                EmployeeId= employeeDto.EmployeeId,
                EmployeeName = employeeDto.EmployeeName,
                EmployeeDesignation = employeeDto.EmployeeDesignation,
                EmployeeDOB = employeeDto.EmployeeDOB,
            };

            var result = _employeeRepository.UpdateEmployee(employeeModel);
            if (result)
            {
                response.Success = true;
                response.Message = "Employee updated successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
            }
            return response;
        }

        public ServiceResponse<string> DeleteEmployee(int employeeId)
        {
            var response = new ServiceResponse<string>();
            var result = _employeeRepository.DeleteEmployee(employeeId);

            if (result)
            {
                response.Success = true;
                response.Message = "Employee deleted successfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
            }

            return response;
        }
    }
}
