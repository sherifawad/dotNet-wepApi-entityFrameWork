using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;

namespace dotNet_wepApi_entityFrameWork.Model.Mapper
{

    [Mapper]
    public partial class EmployeeMapper
    {
        // [MapProperty(nameof(Person.Id), nameof(PersonDto.PersonId))] // Map property with a different name in the target type 
        public partial EmployeeDTO EmployeeToEmployeeDTO(Employee employee);
        public partial Employee EmployeeDTOEmployee(Employee employeeDTO);
    }
}