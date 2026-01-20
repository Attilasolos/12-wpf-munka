using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Employee
    {
        public string Name { get; set; } = "";
        public DateTime? Born { get; set; }
        public string Department { get; set; } = "";
        public decimal Salary { get; set; }
        public bool EnableHomeOffice { get; set; }
        public string PhotoUrl { get; set; } = "";


        public override string ToString()
        {
            return $"{Name} ({Born:yyyy.MM.dd}), {Department}";
        }
    }
}
