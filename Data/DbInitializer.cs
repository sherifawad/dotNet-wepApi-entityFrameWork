using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace dotNet_wepApi_entityFrameWork.Data
{
    public static class DbInitializer
    {
        public static async void Initialize(DataContext context)
        {
            // if (!context.Positions.Any())
            // {
            //     var positionCode = 1;
            //     var positionFaker = new Faker<Position>()
            //         .RuleFor(p => p.Code, _ => positionCode++)
            //         .RuleFor(p => p.Name, f => f.Person.Avatar);

            //     context.Positions.AddRange(positionFaker.Generate(5));
            //     context.SaveChanges();
            // }

            if (!context.Employees.Any())
            {
                var positions = await context.Positions.ToListAsync();
                if (positions.Count < 1)
                {
                    var positionCode = 0;
                    var positionFaker = new Faker<Position>()
                        .RuleFor(p => p.Code, _ => positionCode++)
                        .RuleFor(p => p.Name, f => f.Person.Avatar);

                    context.Positions.AddRange(positionFaker.Generate(5));
                    context.SaveChanges();
                }
                var employeeCode = 1;
                var employeeFaker = new Faker<Employee>()
                    .RuleFor(e => e.Code, _ => employeeCode++)
                    .RuleFor(e => e.Name, f => f.Person.FullName)
                    .RuleFor(e => e.HiringDate, f => f.Person.DateOfBirth)
                    .RuleFor(e => e.SalaryStatus, f => f.Random.Enum<SalaryStatus>())
                    .RuleFor(e => e.PositionCode, f => f.Random.ListItem(positions).Code);
                // .RuleFor(e => e.Position, f => f.Random.ListItem(positions));

                var employees = employeeFaker.Generate(20);

                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }
    }
}
