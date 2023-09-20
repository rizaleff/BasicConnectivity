using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    internal class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }


        public readonly string connectionString = "Data Source=DESKTOP-2A0I62H; Database=db_hr; Connect Timeout=; Integrated Security=True";

        public override string ToString()
        {
            return $"{Id} - {Name} - {RegionId}";
        }
        public List<Country> GetAll()
        {
            var countries = new List<Country>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_countries";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        countries.Add(new Country
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            RegionId = reader.GetInt32(2)
                        });
                    }
                    reader.Close();
                    connection.Close();

                    return countries;
                }
                reader.Close();
                connection.Close();

                return new List<Country>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Country>();
        }
        public Country GetById(int id)
        {
            Country country = new Country();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_countries WHERE id = @id";

            try
            {
                var pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.Value = id;
                pId.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(pId);

                connection.Open();
                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        country.Id = reader.GetInt32(0);
                        country.Name = reader.GetString(1);
                        country.RegionId = reader.GetInt32(2);
                    }
                    reader.Close();
                    connection.Close();
                    return country;
                }
                reader.Close();
                connection.Close();
                return new Country();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Country();
        }

        public string Insert(int id, string name, int regionId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_countries VALUES (@id, @name, @region_id)";

            try
            {
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@id", regionId));

                connection.Open();
                using var transaction = connection.BeginTransaction();

                try
                {
                    command.Transaction = transaction;

                    var result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    return result.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Error Transaction: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Update(int id, string name, int regionId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_countries SET name = @name, region_id = @regionId WHERE id = @id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@region_id", regionId));

                connection.Open();
                using var transaction = connection.BeginTransaction();

                try
                {
                    command.Transaction = transaction;

                    var result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    return result.ToString();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Error Transaction: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "DELETE FROM tbl_countries WHERE id = @id";

            try
            {
                var pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.Value = id;
                pId.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(pId);

                connection.Open();

                using var transaction = connection.BeginTransaction();

                try
                {
                    command.Transaction = transaction;
                    var result = command.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();

                    return result.ToString();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return $"Error Transaction: {ex.Message}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
