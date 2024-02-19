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
        Task<ServiceResponse<EmployeeDTO>> AddEmployee(EmployeeDTO newEmployee);
        Task<ServiceResponse<EmployeeDTO>> UpdateEmployee(int code, EmployeeDTO updatedEmployee);
        Task<ServiceResponse<EmployeeDTO>> DeleteEmployee(int code);
        Task<ServiceResponse<int>> GetMaxCode();
    }
}
