using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Helpers;

namespace dotNet_wepApi_entityFrameWork.Repository.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(QueryObject query);
        Task<List<Employee>> GetFilteredAsync(RootFilter query);
        Task<Employee?> GetByCodeAsync(int code);
        Task<Employee?> CreateAsync(Employee employeeModel);
        Task<Employee?> UpdateAsync(int code, EmployeeDTO employeeDto);
        Task<Employee?> DeleteAsync(int code);
        Task<List<int>?> DeleteManyAsync(List<int> codes);
        Task<int> GetMaxCodeAsync();
        Task<bool> EmployeeExists(int code);
    }
}
