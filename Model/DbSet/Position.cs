using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dotNet_wepApi_entityFrameWork.Model
{
    public class Position
    {
        public int Code { get; set; }
        public required string Name { get; set; }
    }
}
