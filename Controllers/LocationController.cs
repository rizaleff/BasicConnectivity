using BasicConnectivity.Model;
using BasicConnectivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity.Controllers
{
    public class LocationController
    {
        private Location _location;
        private LocationView _locationView;

        public LocationController(Location location, LocationView locationView)
        {
            _location = location;
            _locationView = locationView;
        }

        public void GetAll()
        {

            var results = _location.GetAll();

            //Pembuatan Condition diawali dari kondisi Error
            if (!results.Any())
            {
                Console.WriteLine("No Data Found");
            }
            else
            {
                _locationView.List(results, "countries");
            }
        }

        public void Insert()
        {
            var inputLocation = new Location();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    inputLocation = _locationView.InsertInput();
                    if (inputLocation.Id.Equals(null))
                    {
                        Console.WriteLine("Location ID cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputLocation.StreetAddress))
                    {
                        Console.WriteLine("Street Address cannot be empty");
                        continue;
                    }                    
                    if (string.IsNullOrEmpty(inputLocation.PostalCode))
                    {
                        Console.WriteLine("postal code cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputLocation.StateProvince))
                    {
                        Console.WriteLine("State province cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputLocation.City))
                    {
                        Console.WriteLine("City cannot be empty");
                        continue;
                    }
                    if (inputLocation.CountryId.Equals(null))
                    {
                        Console.WriteLine("Country Region ID cannot be empty");
                        continue;
                    }

                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            var result = _location.Insert(inputLocation);

            _locationView.Transaction(result);
        }

        public void Update()
        {
            var inputLocation = new Location();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    inputLocation = _locationView.UpdateInput();
                    if (inputLocation.Id.Equals(null))
                    {
                        Console.WriteLine("Location ID cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputLocation.StreetAddress))
                    {
                        Console.WriteLine("Street Address cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputLocation.PostalCode))
                    {
                        Console.WriteLine("postal code cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputLocation.StateProvince))
                    {
                        Console.WriteLine("State province cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputLocation.City))
                    {
                        Console.WriteLine("City cannot be empty");
                        continue;
                    }
                    if (inputLocation.CountryId.Equals(null))
                    {
                        Console.WriteLine("Country Region ID cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            var result = _location.Update(inputLocation);
            _locationView.Transaction(result);
        }
        public void Delete()
        {
            int id = 0;
            var isTrue = true;

            while (isTrue)
            {
                try
                {
                    id = _locationView.DeleteInput();
                    Console.WriteLine(id);
                    if (id.Equals(null))
                    {
                        Console.WriteLine("Location ID cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            var result = _location.Delete(id);
            _locationView.Transaction(result);
        }
    }
}
