using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Helpers;
using dotNet_wepApi_entityFrameWork.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;

namespace dotNet_wepApi_entityFrameWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<EmployeeDTO>>>> Get(
            [FromQuery] QueryObject query
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await employeeService.GetAllEmployees(query));
        }

        [HttpGet("{code:int}")]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> GetByCode(
            [FromRoute] int code
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await employeeService.GetMaxCode();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> AddEmployee(
            [FromBody] EmployeeDTO newEmployee
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await employeeService.AddEmployee(newEmployee);
            if (result.Data is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
            }
            return CreatedAtAction(nameof(GetByCode), result);
        }

        [HttpPost("DeleteMany")]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> DeleteMany(
            [FromBody] IEnumerable<int> codesList
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await employeeService.DeleteMany(codesList);
            if (result.Data is null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPut("{code:int}")]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> UpdateEmployee(
            [FromRoute] int code,
            [FromBody] EmployeeDTO updatedEmployee
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await employeeService.UpdateEmployee(code, updatedEmployee);
            if (result.Data is null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{code:int}")]
        public async Task<ActionResult<ServiceResponse<EmployeeDTO>>> DeleteEmployee(
            [FromRoute] int code
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await employeeService.DeleteEmployee(code);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
