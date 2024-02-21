using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Data;
using dotNet_wepApi_entityFrameWork.Helpers;
using dotNet_wepApi_entityFrameWork.Repository.EmployeeRepository;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace dotNet_wepApi_entityFrameWork.Services.EmployeeService
{
    public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
    {
        public async Task<ServiceResponse<EmployeeDTO?>> AddEmployee(EmployeeDTO newEmployee)
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO?>();
            try
            {
                var employee = newEmployee.ToEmployee();

                var result = await repository.CreateAsync(employee);

                if (result is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Failed To Create";
                }
                else
                {
                    serviceResponse.Data = result.ToEmployeeDto();
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeeDTO?>> DeleteEmployee(int code)
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO?>();
            try
            {
                var result = await repository.DeleteAsync(code);
                if (result is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Failed To Delete";
                }
                else
                {
                    serviceResponse.Data = result.ToEmployeeDto();
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IList<int>>> DeleteMany(IEnumerable<int> codesList)
        {
            var serviceResponse = new ServiceResponse<IList<int>>();

            try
            {
                var result = await repository.DeleteManyAsync(codesList.ToList());
                if (result is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Failed To Delete";
                }
                else
                {
                    serviceResponse.Data = result;
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IList<EmployeeDTO>>> GetAllEmployees(QueryObject? query)
        {
            var serviceResponse = new ServiceResponse<IList<EmployeeDTO>>();
            try
            {
                var employees = await repository.GetAllAsync(query);

                var result = employees.Select(e => e.ToEmployeeDto()).ToList();
                serviceResponse.Data = result;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeeDTO?>> GetEmployeeByCode(int code)
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO?>();
            try
            {
                var employee = await repository.GetByCodeAsync(code);
                if (employee is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Employee Not Found";
                }
                else
                {
                    serviceResponse.Data = employee.ToEmployeeDto();
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> GetMaxCode()
        {
            var serviceResponse = new ServiceResponse<int>();
            try
            {
                var result = await repository.GetMaxCodeAsync();
                serviceResponse.Data = result;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeeDTO?>> UpdateEmployee(
            int code,
            EmployeeDTO updatedEmployee
        )
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO?>();
            try
            {
                var employee = await repository.UpdateAsync(code, updatedEmployee);

                if (employee is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Failed To Update";
                }
                else
                {
                    serviceResponse.Data = employee.ToEmployeeDto();
                    serviceResponse.Success = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IList<EmployeeDTO>>> GetFilteredEmployees(
            RootFilter query
        )
        {
            var serviceResponse = new ServiceResponse<IList<EmployeeDTO>>();
            try
            {
                var employees = await repository.GetFilteredAsync(query);

                var result = employees.Select(e => e.ToEmployeeDto()).ToList();
                serviceResponse.Data = result;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public ServiceResponse<IQueryable<Employee>> GetAllQueryableAsync()
        {
            var serviceResponse = new ServiceResponse<IQueryable<Employee>>();
            try
            {
                var result = repository.GetAllQueryableAsync();
                serviceResponse.Data = result;
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
