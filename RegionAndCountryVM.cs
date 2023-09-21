using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    internal class RegionAndCountryVM
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public int RegionId { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return $"{CountryId} - {CountryName} - {RegionName} - {RegionId} - {City}";
        }
    }
}
