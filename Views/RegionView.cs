using BasicConnectivity.Model;
using BasicConnectivity.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity.Views
{
    public class RegionView : GeneralView
    {
        public string InsertInput()
        {
            Console.Write("Insert Region Name   : ");
            var name = Console.ReadLine();

            return name;
        }

        public Region UpdateInput()
        {
            Console.Write("Insert region id     : ");
            var id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Insert region name");
            var name = Console.ReadLine();

            return new Region
            {
                Id = id,
                Name = name
            };
        }

        public int DeleteInput()
        {
            Console.Write("Insert region id     : ");
            int.TryParse(Console.ReadLine(), out int id);

            return id;
        }
    }
}
