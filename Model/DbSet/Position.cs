using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_wepApi_entityFrameWork.Model
{
    public class Position
    {
        public int PositionCode { get; set; }
        public required string PositionName { get; set; }
    }
}