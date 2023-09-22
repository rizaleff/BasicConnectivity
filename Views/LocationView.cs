using BasicConnectivity.Model;
using BasicConnectivity.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity.Views
{
    public class LocationView : GeneralView
    {
        public Location InsertInput()
        {
            Console.Write("Insert Location Id       : ");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Insert Street Address    : ");
            var streetAddress = Console.ReadLine();
            Console.Write("Insert Postal Code       : ");
            var postalCode = Console.ReadLine();
            Console.Write("Insert State Province    : ");
            var stateProvince = Console.ReadLine();
            Console.Write("Insert City              : ");
            var city = Console.ReadLine();
            Console.Write("Insert Country ID        : ");
            int.TryParse(Console.ReadLine(), out int countryId);

            return new Location
            {
                Id = id,
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                StateProvince = stateProvince,
                City = city,
                CountryId = countryId
            };
        }

        public Location UpdateInput()
        {
            Console.Write("Insert Location Id       : ");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Insert Street Address    : ");
            var streetAddress = Console.ReadLine();
            Console.Write("Insert Postal Code       : ");
            var postalCode = Console.ReadLine();
            Console.Write("Insert State Province    : ");
            var stateProvince = Console.ReadLine();
            Console.Write("Insert City              : ");
            var city = Console.ReadLine();
            Console.Write("Insert Country ID        : ");
            int.TryParse(Console.ReadLine(), out int countryId);

            return new Location
            {
                Id = id,
                StreetAddress = streetAddress,
                PostalCode = postalCode,
                StateProvince = stateProvince,
                City = city,
                CountryId = countryId
            };
        }

        public int DeleteInput()
        {
            Console.Write("Insert country id     : ");
            int.TryParse(Console.ReadLine(), out int id);

            return id;
        }
    }
}
