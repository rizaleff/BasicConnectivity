using BasicConnectivity.Model;
using BasicConnectivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity.Controllers
{  
/*
 * Controller buat sederhana Mungkin
 */
    internal class RegionController
    {
        // underscore digunakan untuk menamai variabel private
        private Region _region;
        private RegionView _regionView;

        public RegionController (Region region, RegionView regionView)
        {
            _region = region;
            _regionView = regionView;
        }

        public void GetAll()
        {
            
            var results = _region.GetAll();

            //Pembuatan Condition diawali dari kondisi Error
            if(!results.Any())
            {
                Console.WriteLine("No Data Found");
            }
            else
            {
                _regionView.List(results, "regions");
            }
        }

        public void Insert()
        {
            string input = "";
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    input = _regionView.InsertInput();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Region name cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            var result = _region.Insert(new Region
            {
                Id = 0,
                Name = input
            });

            _regionView.Transaction(result);
        }
        
        public void Update()
        {
            var region = new Region();
            var isTrue = true;
            while (isTrue)
            {
                try
                {
                    region = _regionView.UpdateInput();
                    if (string.IsNullOrEmpty(region.Name))
                    {
                        Console.WriteLine("Region name cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            var result = _region.Update(region);
            _regionView.Transaction(result);
        }
        public void Delete()
        {
            int id = 0;
            var isTrue = true;

            while (isTrue)
            {
                try
                {
                    id = _regionView.DeleteInput();
                    Console.WriteLine(id);
                    if (id.Equals(null))
                    {
                        Console.WriteLine("Region ID cannot be empty");
                        continue;
                    }
                    isTrue = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            var result = _region.Delete(id);
            _regionView.Transaction(result);
        }
    }
}
