using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    internal class Department
    {
        public readonly string connectionString = "Data Source=DESKTOP-2A0I62H; Database=db_hr; Connect Timeout=; Integrated Security=True";

        public int Id { get; set; }
        public string Name { get; set; }    
        public int ManagerId {  get; set; }
        public int LocationId { get; set; }

        public List<Department> GetAll()
        {
            var departments = new List<Department>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_departments";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            ManagerId = reader.GetInt32(2),
                            LocationId = reader.GetInt32(3)
                        });
                    }
                    reader.Close();
                    connection.Close();

                    return departments;
                }
                reader.Close();
                connection.Close();

                return new List<Department>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Department>();
        }
        public Department GetById(int id)
        {
            Department department = new Department();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_departments WHERE id = @id";

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
                        department.Id = reader.GetInt32(0);
                        department.Name = reader.GetString(1);
                        department.ManagerId = reader.GetInt32(2);
                        department.LocationId = reader.GetInt32(3);
                    }
                    reader.Close();
                    connection.Close();
                    return department;
                }
                reader.Close();
                connection.Close();
                return new Department();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Department();
        }

        public string Insert(int id, string name, int managerId, int locationId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_departments VALUES (@id, @name, @manager_id)";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@manager_id", managerId));
                command.Parameters.Add(new SqlParameter("@location_id", locationId));

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

        public string Update(int id, string name, int managerId, int locationId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_departments SET name = @name, manager_id = @manager_id, location_id=@location_id WHERE id = @id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@name", name));
                command.Parameters.Add(new SqlParameter("@manager_id", managerId));
                command.Parameters.Add(new SqlParameter("@location_id", locationId));

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
            command.CommandText = "DELETE FROM tbl_departments WHERE id = @id";

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
