using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_wepApi_entityFrameWork.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<ServiceResponse<IList<EmployeeDTO>>> AddEmployee(EmployeeDTO newEmployee)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<IList<EmployeeDTO>>> DeleteEmployee(int code)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<IList<EmployeeDTO>>> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<EmployeeDTO>> GetEmployeeByCode(int code)
        {
            var serviceResponse = new ServiceResponse<EmployeeDTO>();
            // var dbEmployee = { };
            // serviceResponse.Data = _mapper.Map<GetEmployeesResponseDTO>(dbEmployee);
            return serviceResponse;
        }

        public async Task<ServiceResponse<EmployeeDTO>> UpdateEmployee(EmployeeDTO updatedEmployee)
        {
            throw new NotImplementedException();
        }
    }
}

