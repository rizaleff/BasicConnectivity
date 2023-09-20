using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasicConnectivity
{
    internal class Job : Table<Job>
    {
        public readonly string connectionString = "Data Source=DESKTOP-2A0I62H; Database=db_hr; Connect Timeout=; Integrated Security=True";

        public int Id { get; set; }
        public string Title { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }


        public override string ToString()
        {
            return $"{Id} - {Title} - {MinSalary} - {MaxSalary}";
        }
        public override List<Job> GetAll()
        {
            var jobs = new List<Job>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_jobs";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        jobs.Add(new Job
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            MinSalary = reader.GetInt32(2),
                            MaxSalary = reader.GetInt32(3)
                        });
                    }
                    reader.Close();
                    connection.Close();

                    return jobs;
                }
                reader.Close();
                connection.Close();

                return new List<Job>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Job>();
        }
        public Job GetById(int id)
        {
            Job job = new Job();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_jobs WHERE id = @id";

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
                        job.Id = reader.GetInt32(0);
                        job.Title = reader.GetString(1);
                        job.MinSalary = reader.GetInt32(2);
                        job.MaxSalary = reader.GetInt32(3);
                    }
                    reader.Close();
                    connection.Close();
                    return job;
                }
                reader.Close();
                connection.Close();
                return new Job();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Job();
        }

        public string Insert(int id, string title, int minSalary, int maxSalary)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_jobs VALUES (@id, @title, @min_salary, @max_salary)";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@title", title));
                command.Parameters.Add(new SqlParameter(" @min_salary", minSalary));
                command.Parameters.Add(new SqlParameter(" @min_salary", maxSalary));


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

        public string Update(int id, string title, int minSalary, int maxSalary)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_jobs SET id=@id, title=@title, min_salary=@min_salary, max_salary=@max_salary WHERE id = @id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@title", title));
                command.Parameters.Add(new SqlParameter(" @min_salary", minSalary));
                command.Parameters.Add(new SqlParameter(" @min_salary", maxSalary));

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
            command.CommandText = "DELETE FROM tbl_jobs WHERE id = @id";

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

        public override void Print()
        {
            Console.WriteLine($"Job Name : {Title}");

        }


    }
}
