using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dotNet_wepApi_entityFrameWork.Model;

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
    }

    public enum SalaryStatus
    {
        VALID,
        NOT_VALID
    }
}
