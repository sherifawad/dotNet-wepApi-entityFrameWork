using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Data;
using dotNet_wepApi_entityFrameWork.Model.Mapper;
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
                var employee = EmployeeMapper.EmployeeDTOEmployee(newEmployee);
                var duplicateCode = await dataContext.Employees.FirstOrDefaultAsync(e =>
                    e.Code == employee.Code
                );
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
                serviceResponse.Data = EmployeeMapper.EmployeeToEmployeeDTO(result!);
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
                    await dataContext.Employees.FirstOrDefaultAsync(e => e.Code == code)
                    ?? throw new Exception($"employee not found.");
                dataContext.Employees.Remove(employee);

                await dataContext.SaveChangesAsync();
                var result = await dataContext.Employees.FirstOrDefaultAsync(e =>
                    e.Code == employee.Code
                );
                serviceResponse.Data = EmployeeMapper.EmployeeToEmployeeDTO(result!);
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

                var result = employees
                    .Select(e => EmployeeMapper.EmployeeToEmployeeDTO(e))
                    .ToList();
                serviceResponse.Data = result;
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
                serviceResponse.Data = EmployeeMapper.EmployeeToEmployeeDTO(employee);
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
                var employeeToUpdate = EmployeeMapper.EmployeeDTOEmployee(updatedEmployee);
                var employee =
                    await dataContext.Employees.FirstOrDefaultAsync(e =>
                        e.Code == employeeToUpdate.Code
                    ) ?? throw new Exception("employee not found.");

                if (employeeToUpdate.Code == 0)
                {
                    throw new Exception("Invalid Code");
                }
                if (employeeToUpdate.Code != code)
                {
                    var duplicateCode =
                        await dataContext.Employees.FirstOrDefaultAsync(e => e.Code == code)
                        ?? throw new Exception("Code Duplicate");
                }

                if (employeeToUpdate.PositionCode is not null)
                {
                    var position =
                        await dataContext.Positions.FirstOrDefaultAsync(p =>
                            p.Code == employeeToUpdate.PositionCode
                        ) ?? throw new Exception("Position not Found");
                }

                employee.Code = employeeToUpdate.Code;
                employee.Name = employeeToUpdate.Name;
                employee.SalaryStatus = employeeToUpdate.SalaryStatus;
                employee.HiringDate = employeeToUpdate.HiringDate;
                employee.PositionCode = employeeToUpdate.PositionCode;
                await dataContext.Employees.AddAsync(employee);
                await dataContext.SaveChangesAsync();

                serviceResponse.Data = EmployeeMapper.EmployeeToEmployeeDTO(employee!);
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
