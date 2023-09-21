using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    internal class JobHistory
    {
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int JobID { get; set; }
        public int DepartmentId { get; set; }

        public override string ToString()
        {
            return $"{EmployeeID} - {StartDate} - {EndDate} - {JobID} - {DepartmentId}";
        }
        public List<JobHistory> GetAll()
        {
            var jobHistories = new List<JobHistory>();

            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_job_histories";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        jobHistories.Add(new JobHistory
                        {
                            EmployeeID = reader.GetInt32(0),
                            StartDate = reader.GetDateTime(1),
                            EndDate = reader.GetDateTime(2),
                            JobID = reader.GetInt32(3),
                            DepartmentId = reader.GetInt32(4),
                        }); ;
                    }
                    reader.Close();
                    connection.Close();

                    return jobHistories;
                }
                reader.Close();
                connection.Close();

                return new List<JobHistory>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<JobHistory>();
        }
        public JobHistory GetById(int id)
        {
            JobHistory jobHistory = new JobHistory();
            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_job_histories WHERE id = @id";

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
                        jobHistory.EmployeeID = reader.GetInt32(0);
                        jobHistory.StartDate = reader.GetDateTime(1);
                        jobHistory.EndDate = reader.GetDateTime(2);
                        jobHistory.JobID = reader.GetInt32(3);
                        jobHistory.DepartmentId = reader.GetInt32(4);
                    }
                    reader.Close();
                    connection.Close();
                    return jobHistory;
                }
                reader.Close();
                connection.Close();
                return new JobHistory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new JobHistory();
        }

        public string Insert(int employeeId, DateTime startDate, DateTime endDate, int jobId, int departmentId)
        {
            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_job_histories VALUES (@employee_id, @start_date, @end_date, @job_id, @department_id)";

            try
            {
                command.Parameters.Add(new SqlParameter("@employee_id", employeeId));
                command.Parameters.Add(new SqlParameter("@start_date", startDate));
                command.Parameters.Add(new SqlParameter("@end_date", endDate));
                command.Parameters.Add(new SqlParameter("@job_id", jobId));
                command.Parameters.Add(new SqlParameter("@department_id", departmentId));


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

        public string Update(int employeeId, DateTime startDate, DateTime endDate, int jobId, int departmentId)
        {
            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_job_histories SET @end_date, @job_id, @department_id WHERE employee_id=@employee_id, start_date=@start_date";

            try
            {
                command.Parameters.Add(new SqlParameter("@employee_id", employeeId));
                command.Parameters.Add(new SqlParameter("@start_date", startDate));
                command.Parameters.Add(new SqlParameter("@end_date", endDate));
                command.Parameters.Add(new SqlParameter("@job_id", jobId));
                command.Parameters.Add(new SqlParameter("@department_id", departmentId));

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
            using var connection = Provider.GetConnection();
            using var command = Provider.GetCommand();

            command.Connection = connection;
            command.CommandText = "DELETE FROM tbl_job_histories WHERE id = @id";

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
