using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Model;
using dotNet_wepApi_entityFrameWork.Model.Dtos.Position;

namespace dotNet_wepApi_entityFrameWork.Dtos
{
    public class EmployeeDTO
    {
        public int Code { get; set; }
        public required string Name { get; set; }
        public int? PositionCode { get; set; }
        public PositionDTO? Position { get; set; }
        public required SalaryStatus SalaryStatus { get; set; }
        public DateTime HiringDate { get; set; }

        public Employee ToEmployee()
        {
            return new Employee
            {
                Code = Code,
                Name = Name,
                SalaryStatus = SalaryStatus,
                PositionCode = PositionCode,
                HiringDate = HiringDate,
                Position = Position is null
                    ? null
                    : new Position { Name = Position.PositionName, Code = Position.PositionCode }
            };
        }

        public static EmployeeDTO FromEmployee(Employee employee)
        {
            return new EmployeeDTO
            {
                Code = employee.Code,
                Name = employee.Name,
                SalaryStatus = employee.SalaryStatus,
                PositionCode = employee.PositionCode,
                HiringDate = employee.HiringDate,
                Position = employee.Position is null
                    ? null
                    : new PositionDTO
                    {
                        PositionName = employee.Position.Name,
                        PositionCode = employee.Position.Code
                    }
            };
        }
    }
}
