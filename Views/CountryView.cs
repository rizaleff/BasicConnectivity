using BasicConnectivity.Model;
using BasicConnectivity.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity.Views
{
    public class CountryView : GeneralView
    {
        public Country InsertInput()
        {
            Console.Write("Insert Country Id   : ");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Insert Country Name   : ");
            var name = Console.ReadLine();
            Console.Write("Insert Region id        : ");
            int.TryParse(Console.ReadLine(), out int regionId);

            return new Country
            {
                Id = id,
                Name = name,
                RegionId = regionId
            };
        }

        public Country UpdateInput()
        {
            Console.Write("Insert country id        : ");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Insert country name      :");
            var name = Console.ReadLine();
            Console.Write("Insert Region id        : ");
            int.TryParse(Console.ReadLine(), out int regionId);

            return new Country
            {
                Id = id,
                Name = name,
                RegionId = regionId
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
