using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_wepApi_entityFrameWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<EmployeeDTO>>>> Get()
        {
            return Ok(await employeeService.GetAllEmployees());
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> GetSingle(int code)
        {
            var result = await employeeService.GetEmployeeByCode(code);
            if (result.Data is null)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("GetMaxCode")]
        public async Task<ActionResult<ServiceResponse<int>>> GetMaxCode()
        {
            var result = await employeeService.GetMaxCode();
            if (result.Message != string.Empty)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> AddEmployee(
            EmployeeDTO newEmployee
        )
        {
            var result = await employeeService.AddEmployee(newEmployee);
            if (result.Data is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            }
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> UpdateEmployee(
            int code,
            EmployeeDTO updatedEmployee
        )
        {
            var result = await employeeService.UpdateEmployee(code, updatedEmployee);
            if (result.Data is null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> DeleteEmployee(int code)
        {
            var response = await employeeService.DeleteEmployee(code);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
