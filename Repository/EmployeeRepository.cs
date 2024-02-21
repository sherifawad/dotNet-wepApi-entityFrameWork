using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Data;
using dotNet_wepApi_entityFrameWork.Helpers;
using Microsoft.EntityFrameworkCore;

namespace dotNet_wepApi_entityFrameWork.Repository.EmployeeRepository
{
    public class EmployeeRepository(DataContext dataContext) : IEmployeeRepository
    {
        public async Task<Employee?> CreateAsync(Employee employeeModel)
        {
            if (employeeModel.Code < 1)
            {
                return null;
            }
            var existingEmployee = await dataContext.Employees.FirstOrDefaultAsync(e =>
                e.Code == employeeModel.Code
            );

            if (existingEmployee != null)
            {
                return null;
            }
            if (employeeModel.PositionCode != null)
            {
                var positionExist = await dataContext.Positions.FirstOrDefaultAsync(p =>
                    p.Code == employeeModel.PositionCode
                );
                if (positionExist == null)
                {
                    return null;
                }
            }

            await dataContext.Employees.AddAsync(employeeModel);
            await dataContext.SaveChangesAsync();
            return employeeModel;
        }

        public async Task<Employee?> DeleteAsync(int code)
        {
            var employeeModel = await dataContext.Employees.FirstOrDefaultAsync(e =>
                e.Code == code
            );

            if (employeeModel == null)
            {
                return null;
            }

            dataContext.Employees.Remove(employeeModel);
            await dataContext.SaveChangesAsync();
            return employeeModel;
        }

        public async Task<List<int>?> DeleteManyAsync(List<int> codes)
        {
            await dataContext
                .Employees.Where(e => codes.Any(c => c == e.Code))
                .ExecuteDeleteAsync();
            return codes;
        }

        public Task<bool> EmployeeExists(int code)
        {
            return dataContext.Employees.AnyAsync(e => e.Code == code);
        }

        public async Task<List<Employee>> GetAllAsync(QueryObject? query)
        {
            var employees = dataContext
                .Employees.Include(e => e.Position)
                .AsQueryable()
                .OrderBy(s => s.Code);
            if (query is null)
            {
                return await employees.ToListAsync();
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    employees = query.IsDescending
                        ? employees.OrderByDescending(s => s.Code)
                        : employees.OrderBy(s => s.Code);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await employees.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public IQueryable<Employee> GetAllQueryableAsync()
        {
            return dataContext.Employees.Include(e => e.Position).AsQueryable();
        }

        public async Task<Employee?> GetByCodeAsync(int code)
        {
            return await dataContext
                .Employees.Include(c => c.Position)
                .FirstOrDefaultAsync(i => i.Code == code);
        }

        public async Task<List<Employee>> GetFilteredAsync(RootFilter query)
        {
            if (query.Filters == null || query.Filters.Count < 1)
            {
                var employees = dataContext.Employees.Include(e => e.Position).AsQueryable();
                return await employees.ToListAsync();
            }
            else
            {
                var filterExpression = CompositeFilter<Employee>.GetFilterExpression(query);
                if (filterExpression is null)
                {
                    var result = dataContext.Employees.Include(e => e.Position).AsQueryable();
                    return await result.ToListAsync();
                }
                var employees = dataContext
                    .Employees.Where(filterExpression)
                    .Include(e => e.Position)
                    .AsQueryable();
                return await employees.ToListAsync();
            }
        }

        public async Task<int> GetMaxCodeAsync()
        {
            return await dataContext.Employees.MaxAsync(e => e.Code);
        }

        public async Task<Employee?> UpdateAsync(int code, EmployeeDTO employeeDto)
        {
            var existingEmployee = await dataContext.Employees.FirstOrDefaultAsync(e =>
                e.Code == code
            );

            if (existingEmployee == null)
            {
                return null;
            }

            if (code != employeeDto.Code)
            {
                var codeDuplicate = await dataContext.Employees.FirstOrDefaultAsync(e =>
                    e.Code == employeeDto.Code
                );

                if (codeDuplicate == null)
                {
                    existingEmployee.Code = employeeDto.Code;
                }
                else
                {
                    return null;
                }
            }

            existingEmployee.HiringDate = employeeDto.HiringDate;
            existingEmployee.SalaryStatus = employeeDto.SalaryStatus;
            existingEmployee.Name = employeeDto.Name;
            existingEmployee.PositionCode = employeeDto.PositionCode;

            await dataContext.SaveChangesAsync();

            return existingEmployee;
        }
    }
}
