using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Helpers;

namespace dotNet_wepApi_entityFrameWork.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<IList<EmployeeDTO>>> GetAllEmployees(QueryObject query);
        Task<ServiceResponse<EmployeeDTO?>> GetEmployeeByCode(int code);
        Task<ServiceResponse<EmployeeDTO?>> AddEmployee(EmployeeDTO newEmployee);
        Task<ServiceResponse<EmployeeDTO?>> UpdateEmployee(int code, EmployeeDTO updatedEmployee);
        Task<ServiceResponse<EmployeeDTO?>> DeleteEmployee(int code);
        Task<ServiceResponse<IList<int>>> DeleteMany(IEnumerable<int> codesList);
        Task<ServiceResponse<int>> GetMaxCode();
    }
}
