using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConnectivity
{
    internal class Employee : Table<Employee>
    {
        public readonly string connectionString = "Data Source=DESKTOP-2A0I62H; Database=db_hr; Connect Timeout=; Integrated Security=True";

        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? HireDate { get; set; }
        public int? JobId { get; set; }
        public double? Salary { get; set; }
        public double? ComissionPct { get; set; }

        public int? DepartmentId { get; set; }
        public int? ManagerId { get; set; }

        public override List<Employee> GetAll()
        {
            var employees = new List<Employee>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_employees";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetValue(10));


                        employees.Add(new Employee
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            HireDate = reader.GetDateTime(5),
                            JobId = reader.GetInt32(6),
                            Salary = reader.GetDouble(7),
                            ComissionPct = reader.GetDouble(8),
                            DepartmentId = reader.GetInt32(9),
                            //ManagerId = reader.GetInt32(10)
                        });
                    }
                    reader.Close();
                    connection.Close();

                    return employees;
                }
                reader.Close();
                connection.Close();

                return new List<Employee>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Employee>();
        }
        public Employee GetById(int id)
        {
            Employee employee = new Employee();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_employees WHERE id = @id";

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
                        employee.Id = reader.GetInt32(0);
                        employee.FirstName = reader.GetString(1);
                        employee.LastName = reader.GetString(2);
                        employee.Email = reader.GetString(3);
                        employee.PhoneNumber = reader.GetString(4);
                        employee.HireDate = reader.GetDateTime(5);
                        employee.JobId = reader.GetInt32(6);
                        employee.Salary = reader.GetFloat(7);
                        employee.ComissionPct = reader.GetFloat(8);
                        employee.DepartmentId = reader.GetInt32(9);
                        employee.ManagerId = reader.GetInt32(10);
                    }
                    reader.Close();
                    connection.Close();
                    return employee;
                }
                reader.Close();
                connection.Close();
                return new Employee();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Employee();
        }

        public string Insert(int id, string firstName, string lastName, string email, string phoneNumber, DateTime hireDate, int jobId, float salary, float comissionPct, int departmentId, int managerId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_employees VALUES (@id, @first_name, @last_name, @email, @phone_number, @hire_date, @job_id, @salary, @comission_pct, @department_id, @manager_id)";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@first_name", firstName));
                command.Parameters.Add(new SqlParameter("@last_name", lastName));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@phone_number", phoneNumber));
                command.Parameters.Add(new SqlParameter("@hire_date", hireDate));
                command.Parameters.Add(new SqlParameter("@job_id", jobId));
                command.Parameters.Add(new SqlParameter("@salary", salary));
                command.Parameters.Add(new SqlParameter("@comission_pct", comissionPct));
                command.Parameters.Add(new SqlParameter("@manager_id", managerId));

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

        public string Update(int id, string firstName, string lastName, string email, string phoneNumber, DateTime hireDate, int jobId, float salary, float comissionPct, int departmentId, int managerId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_employees SET first_name = @first_name, last_name = @last_name, email = @email, phone_number = @phone_number, hire_date = @hire_date, job_id = @job_id, salary = @salary, comission_pct = @comission_pct, department_id = @department_id, manager_id = @manager_id,  WHERE id = @id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@first_name", firstName));
                command.Parameters.Add(new SqlParameter("@last_name", lastName));
                command.Parameters.Add(new SqlParameter("@email", email));
                command.Parameters.Add(new SqlParameter("@phone_number", phoneNumber));
                command.Parameters.Add(new SqlParameter("@hire_date", hireDate));
                command.Parameters.Add(new SqlParameter("@job_id", jobId));
                command.Parameters.Add(new SqlParameter("@salary", salary));
                command.Parameters.Add(new SqlParameter(" @comission_pct", comissionPct));
                command.Parameters.Add(new SqlParameter(" @department_id", departmentId));
                command.Parameters.Add(new SqlParameter(" @manager_id", managerId));

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
            command.CommandText = "DELETE FROM tbl_employees WHERE id = @id";

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
            Console.WriteLine($"Name: {FirstName}");
        }
    }
}
