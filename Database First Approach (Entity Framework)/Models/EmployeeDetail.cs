using System;
using System.Collections.Generic;

namespace Database_First_Approach_Entity_Framework.Models
{
    public partial class EmployeeDetail
    {
        public int EmpId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Age { get; set; }
        public double Salary { get; set; }
        public string WorkType { get; set; } = null!;
    }
}
