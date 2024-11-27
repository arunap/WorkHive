using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkHive.Application.Employees.Queries.Dtos
{
    public class EmployeesByCafeNameResult : EmployeeResult
    {
        public int DaysWorked { get; set; } 
       
    }
}