using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_wepApi_entityFrameWork.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<IList<EmployeeDTO>>> GetAllEmployees();
        Task<ServiceResponse<EmployeeDTO>> GetEmployeeByCode(int code);
        Task<ServiceResponse<IList<EmployeeDTO>>> AddEmployee(EmployeeDTO newEmployee);
        Task<ServiceResponse<EmployeeDTO>> UpdateEmployee(EmployeeDTO updatedEmployee);
        Task<ServiceResponse<IList<EmployeeDTO>>> DeleteEmployee(int code);
    }
}