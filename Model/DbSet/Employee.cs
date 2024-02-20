using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using dotNet_wepApi_entityFrameWork.Model;
using dotNet_wepApi_entityFrameWork.Model.Dtos.Position;

namespace dotNet_wepApi_entityFrameWork
{
    public class Employee
    {
        public int Code { get; set; }
        public required string Name { get; set; }
        public int? PositionCode { get; set; }
        public Position? Position { get; set; }
        public SalaryStatus SalaryStatus { get; set; } = SalaryStatus.VALID;
        public DateTime HiringDate { get; set; } = DateTime.Now;

        public EmployeeDTO ToEmployeeDto()
        {
            return new EmployeeDTO
            {
                Name = Name,
                SalaryStatus = SalaryStatus,
                Code = Code,
                HiringDate = HiringDate,
                PositionCode = PositionCode,
                Position = Position is null
                    ? null
                    : new PositionDTO { PositionName = Position.Name, PositionCode = Position.Code }
            };
        }
    }

    public enum SalaryStatus
    {
        VALID,
        NOT_VALID
    }
}
