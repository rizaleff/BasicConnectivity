using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    internal class EmployeeInDepartmentVM
    {
        public string DepartmentName {get; set;}
        public int TotalEmployee { get; set;}
        public double MinSalary { get; set;}
        public double MaxSalary { get; set;}
        public double AvgSalary { get; set;}

        public override string ToString()
        {
            return $" {TotalEmployee} - {DepartmentName} - {MinSalary} - {MaxSalary} - {AvgSalary}";
        }
    }
}
