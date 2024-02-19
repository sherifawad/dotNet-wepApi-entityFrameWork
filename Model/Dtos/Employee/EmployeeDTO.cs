using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Model;

namespace dotNet_wepApi_entityFrameWork.Dtos
{
    public class EmployeeDTO
    {
        public int Code { get; set; }
        public required string Name { get; set; }
        public int? PositionCode { get; set; }
        public Position? Position { get; set; }
        public required SalaryStatus SalaryStatus { get; set; }
        public DateTime HiringDate { get; set; }
    }
}
