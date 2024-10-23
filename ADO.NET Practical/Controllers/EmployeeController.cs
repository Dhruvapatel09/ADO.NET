using ADO.NET_Practical.Dtos;
using ADO.NET_Practical.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_Practical.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            var response = _employeeService.GetAllEmployees();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetEmployeeById/{employeeId}")]
        public IActionResult GetEmployeeById(int employeeId)
        {
            var response =  _employeeService.GetEmployeeById(employeeId);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee(AddEmployeeDto employeeDto)
        {
            try
            {
                var response = _employeeService.AddEmployee(employeeDto);
                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Return JSON-formatted error message
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee(EmployeeDto employeeDto)
        {
            var response = _employeeService.UpdateEmployee(employeeDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteEmployee/{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            var response = _employeeService.DeleteEmployee(employeeId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
