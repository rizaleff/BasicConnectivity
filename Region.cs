using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    //Region hanya untuk interaksi ke db, tidak untuk hal lain
    internal class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }

        public List<Region> GetAll()
        {
            var regions = new List<Region>();

            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_regions";

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
        //GET BY ID: regions

        public Region GetById(int id)
        {
            Region region = new Region();
            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_regions WHERE id = @id";

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
                        region.Id = reader.GetInt32(0);
                        region.Name = reader.GetString(1);
                    }
                    reader.Close();
                    connection.Close();
                    return region;
                }
                reader.Close();
                connection.Close();
                return new Region();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Region();
        }

        //INSERT: regions
        public  string Insert(string name)
        {
            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_regions VALUES (@name)";

            try
            {
                var pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.Value = name;
                pName.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(pName);

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
                    //Message bisa di throw di log
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        //UPDATE: regions
        public string Update(int id, string name)
        {
            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_regions SET name = @name WHERE id = @id";

            try
            {
                var pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.Value = id;
                pId.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(pId);

                var pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.Value = name;
                pName.SqlDbType = SqlDbType.VarChar;
                command.Parameters.Add(pName);

                connection.Open();
                using var transaction = connection.BeginTransaction();

                try
                {
                    command.Transaction = transaction;
                    /*
                        ExecuteNonQuery Klo di transaction berperan sbg staging (Klo di versioning)
                        Buat ngecek apa ada masalah atau tidak 
                        RollBack berperan ketika di execue ketika ada

                     */
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

        //DELETE: regions
        public  string Delete(int id)
        {
            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "DELETE FROM tbl_regions WHERE id = @id";

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

                    return result.ToString() ;
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
