using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    public abstract class Table<T>
    {

        public abstract void Print();
        public abstract List<T> GetAll();
    }
}
