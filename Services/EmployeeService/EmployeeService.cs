using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Data;
using Microsoft.EntityFrameworkCore;

namespace dotNet_wepApi_entityFrameWork.Services.EmployeeService
{
    public class EmployeeService(DataContext dataContext) : IEmployeeService
    {
        public async Task<ServiceResponse<EmployeeDTO>> AddEmployee(EmployeeDTO newEmployee)
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO>();
            try
            {
                var employee = newEmployee.ToEmployee();
                var duplicateCode = await dataContext.Employees.FindAsync(employee.Code);
                if (employee.Code == 0)
                {
                    throw new Exception("Invalid Code");
                }
                if (duplicateCode is not null)
                {
                    throw new Exception("Code Duplicate");
                }
                if (employee.PositionCode is not null)
                {
                    var position =
                        await dataContext.Positions.FirstOrDefaultAsync(p =>
                            p.Code == employee.PositionCode
                        ) ?? throw new Exception("Position not Found");
                }
                await dataContext.Employees.AddAsync(employee);
                await dataContext.SaveChangesAsync();
                var result = await dataContext
                    .Employees.Include(e => e.Position)
                    .Where(e => e.Code == employee.Code)
                    .SingleOrDefaultAsync();

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

        public async Task<ServiceResponse<EmployeeDTO>> DeleteEmployee(int code)
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO>();
            try
            {
                var employee =
                    await dataContext.Employees.FindAsync(code)
                    ?? throw new Exception($"employee not found.");
                dataContext.Employees.Remove(employee);

                await dataContext.SaveChangesAsync();
                var result = await dataContext.Employees.FindAsync(employee.Code);
                if (result is null)
                {
                    serviceResponse.Data = employee.ToEmployeeDto();
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Failed To Delete";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<IList<EmployeeDTO>>> GetAllEmployees()
        {
            var serviceResponse = new ServiceResponse<IList<EmployeeDTO>>();
            try
            {
                var employees = await dataContext.Employees.Include(e => e.Position).ToListAsync();

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

        public async Task<ServiceResponse<EmployeeDTO>> GetEmployeeByCode(int code)
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO>();
            try
            {
                var employee =
                    await dataContext
                        .Employees.Where(e => e.Code == code)
                        .Include(e => e.Position)
                        .SingleOrDefaultAsync() ?? throw new Exception($"employee not found.");
                serviceResponse.Data = employee.ToEmployeeDto();
                serviceResponse.Success = true;
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
                var result = await dataContext.Employees.MaxAsync(e => e.Code);
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

        public async Task<ServiceResponse<EmployeeDTO>> UpdateEmployee(
            int code,
            EmployeeDTO updatedEmployee
        )
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO>();
            try
            {
                var employeeToUpdate = updatedEmployee.ToEmployee();
                var employee =
                    await dataContext.Employees.FindAsync(code)
                    ?? throw new Exception("employee not found.");

                if (employeeToUpdate.Code == 0)
                {
                    throw new Exception("Invalid Code");
                }
                if (employeeToUpdate.Code != code)
                {
                    var duplicateCode =
                        await dataContext.Employees.FirstOrDefaultAsync(e =>
                            e.Code == employeeToUpdate.Code
                        ) ?? throw new Exception("Code Duplicate");
                }

                if (employeeToUpdate.PositionCode is not null)
                {
                    var position =
                        await dataContext.Positions.FindAsync(employeeToUpdate.PositionCode)
                        ?? throw new Exception("Position not Found");
                    employee.PositionCode = employeeToUpdate.PositionCode;
                }

                employee.Code = employeeToUpdate.Code;
                employee.Name = employeeToUpdate.Name;
                employee.SalaryStatus = employeeToUpdate.SalaryStatus;
                employee.HiringDate = employeeToUpdate.HiringDate;
                await dataContext.SaveChangesAsync();

                serviceResponse.Data = employee.ToEmployeeDto();
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
