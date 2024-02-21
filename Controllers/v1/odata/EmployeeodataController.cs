using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Asp.Versioning;
using dotNet_wepApi_entityFrameWork.Data;
using dotNet_wepApi_entityFrameWork.Services.EmployeeService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace dotNet_wepApi_entityFrameWork.Controllers.v1.OData
{
    [ApiController]
    [Route("api/odata/employees")]
    public class EmployeeODataController(IEmployeeService service) : ODataController
    {
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<ServiceResponse<List<Employee>>>> Get()
        {
            var Results = await service.GetAllEmployees(null);
            if (Results.Data is null)
            {
                return NotFound();
            }
            return Ok(Results.Data.AsQueryable());
        }
        // public IQueryable Get([FromServices] DataContext context)
        // {
        //     return context.Employees;
        // }
    }
}
