using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_wepApi_entityFrameWork.Model
{
    public class Position
    {
        public int Code { get; set; }
        public required string Name { get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }
    }
}