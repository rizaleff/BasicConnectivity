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
        private static readonly string connectionString = "Data Source=CAMOUFLY;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

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
