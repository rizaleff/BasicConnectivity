using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    internal class Provider
    {
        private static readonly string connectionString = "Data Source=DESKTOP-2A0I62H; Database=db_hr; Connect Timeout=; Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static SqlCommand GetCommand()
        {
            return new SqlCommand();
        }

        public static SqlParameter SetParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }
    }
}
