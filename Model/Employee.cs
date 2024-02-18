using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_wepApi_entityFrameWork
{
    public class Employee
    {
        public int Code { get; set; }
        public required string Name { get; set; } 
        public required Position Position { get; set; }
        public required SalaryStatus SalaryStatus { get; set; }
        public DateTime HiringDate { get; set; }
    }
}