using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet_wepApi_entityFrameWork.Helpers
{
    public class RootFilter
    {
        public List<Filter> Filters { get; set; } = [];
        public string Logic { get; set; } = string.Empty;
    }

    public class Filter
    {
        [Required]
        public string Field { get; set; } = string.Empty;

        [Required]
        public string Operator { get; set; } = string.Empty;

        [Required]
        public object Value { get; set; } = string.Empty;
        public string? Logic { get; set; } // This is the nested "logic" property for the inner filters array
        public List<Filter>? Filters { get; set; } // Nested filters array
    }
}
