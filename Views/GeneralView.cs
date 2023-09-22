using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity.View
{
    public class GeneralView
    {
        public void List<T>(List<T> items, string title)
        {
            Console.WriteLine($"List of {title}");
            Console.WriteLine("---------------");
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public void Single<T>(T item, string title)
        {
            Console.WriteLine($"List of {title}");
            Console.WriteLine("---------------");
            Console.WriteLine(item.ToString());
        }

        public void Transaction(string result)
        {
            int.TryParse(result, out int res);
            if (res > 0)
            {
                Console.WriteLine("Transaction completed successfully");
            }
            else
            {
                Console.WriteLine("Transaction failed");
                Console.WriteLine(result);
            }
        }
    }
}
