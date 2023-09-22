using BasicConnectivity.Model;
using BasicConnectivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity.Controllers
{
    public class CountryController
    {
        private Country _country;
        private CountryView _countryView;

        public CountryController(Country country, CountryView countryView)
        {
            _country = country;
            _countryView = countryView;
        }

        public void GetAll()
        {

            var results = _country.GetAll();

            //Pembuatan Condition diawali dari kondisi Error
            if (!results.Any())
            {
                Console.WriteLine("No Data Found");
            }
            else
            {
                _countryView.List(results, "countries");
            }
        }

        public void Insert()
        {
            var inputCountry = new Country();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    inputCountry = _countryView.InsertInput();
                    if (inputCountry.Id.Equals(null))
                    {
                        Console.WriteLine("Country ID cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputCountry.Name))
                    {
                        Console.WriteLine("Country name cannot be empty");
                        continue;
                    }                    
                    if (inputCountry.RegionId.Equals(null))
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

            var result = _country.Insert(inputCountry);

            _countryView.Transaction(result);
        }

        public void Update()
        {
            var inputCountry = new Country();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    inputCountry = _countryView.UpdateInput();
                    if (inputCountry.Id.Equals(null))
                    {
                        Console.WriteLine("Country ID cannot be empty");
                        continue;
                    }
                    if (string.IsNullOrEmpty(inputCountry.Name))
                    {
                        Console.WriteLine("Country name cannot be empty");
                        continue;
                    }
                    if (inputCountry.RegionId.Equals(null))
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

            var result = _country.Update(inputCountry);
            _countryView.Transaction(result);
        }
        public void Delete()
        {
            int id = 0;
            var isTrue = true;

            while (isTrue)
            {
                try
                {
                    id = _countryView.DeleteInput();
                    Console.WriteLine(id);
                    if (id.Equals(null))
                    {
                        Console.WriteLine("Country ID cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            var result = _country.Delete(id);
            _countryView.Transaction(result);
        }
    }
}
