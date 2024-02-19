using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;

namespace dotNet_wepApi_entityFrameWork.Model.Mapper
{
    [Mapper]
    public static partial class EmployeeMapper
    {
        // [MapProperty(nameof(Person.Id), nameof(PersonDto.PersonId))] // Map property with a different name in the target type
        public static partial EmployeeDTO EmployeeToEmployeeDTO(Employee employee);

        public static partial Employee EmployeeDTOEmployee(EmployeeDTO employeeDTO);
    }
}
