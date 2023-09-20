/*using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    internal class Query<T> 
    {
        public abstract string TableName { get; set; }
        public readonly string connectionString = "Data Source=DESKTOP-2A0I62H; Database=db_hr; Connect Timeout=; Integrated Security=True";

        public List<T> GetAll()
        {
            var t = new List<T>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = $"SELECT * FROM {T.}";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        regions.Add(new Region
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                    reader.Close();
                    connection.Close();
                    return regions;
                }
                reader.Close();
                connection.Close();
                return new List<Region>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new List<Region>();
        }
        
        public override void Print()
        {

        }
    }
}
*/