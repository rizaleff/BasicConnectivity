using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity.ViewModel
{
    internal class EmployeeDetailVM
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public string DepartmentName { get; set; }
        public string StreetAddress { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }


        public override string ToString()
        {
            return $"{EmployeeId} - {FullName} - {Email} - {Phone} - {Salary} - {DepartmentName} - {StreetAddress} - {CountryName} - {RegionName}";
        }
    }
}
