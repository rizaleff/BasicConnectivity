﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasicConnectivity
{
    internal class Location
    {
        public readonly string connectionString = "Data Source=DESKTOP-2A0I62H; Database=db_hr; Connect Timeout=; Integrated Security=True";

        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }

        public override string ToString()
        {
            return $"{Id} - {StreetAddress} - {PostalCode} - {StateProvince} - {City} - {CountryId}";
        }
        public List<Location> GetAll()
        {
            var locations = new List<Location>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_locations";

            try
            {
                connection.Open();

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        locations.Add(new Location
                        {
                            Id = reader.GetInt32(0),
                            StreetAddress = reader.GetString(1),
                            PostalCode = reader.GetString(2),
                            StateProvince = reader.GetString(3),
                            City = reader.GetString(4),
                            CountryId = reader.GetInt32(5)
                        }); ;
                    }
                    reader.Close();
                    connection.Close();

                    return locations;
                }
                reader.Close();
                connection.Close();

                return new List<Location>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return new List<Location>();
        }
        public Location GetById(int id)
        {
            Location location = new Location();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT * FROM tbl_locations WHERE id = @id";

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
                        location.Id = reader.GetInt32(0);
                        location.StreetAddress = reader.GetString(1);
                        location.PostalCode = reader.GetString(2);
                        location.StateProvince = reader.GetString(3);
                        location.City = reader.GetString(4);
                        location.CountryId = reader.GetInt32(5);
                    }
                    reader.Close();
                    connection.Close();
                    return location;
                }
                reader.Close();
                connection.Close();
                return new Location();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return new Location();
        }

        public string Insert(int id, string streetAddress, string postalCode, string stateProvince, string city, int countryId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "INSERT INTO tbl_locations VALUES (@id, @street_address, @postal_code, @state_province, @city, @country_id)";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@street_address", streetAddress));
                command.Parameters.Add(new SqlParameter("@postal_code", postalCode));
                command.Parameters.Add(new SqlParameter("@state_province", stateProvince));
                command.Parameters.Add(new SqlParameter("@city", city));
                command.Parameters.Add(new SqlParameter("@country_id", countryId));



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

        public string Update(int id, string streetAddress, string postalCode, string stateProvince, string city, int countryId)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "UPDATE tbl_locations SET id=@id, street_address=@street_address, postal_code=@postal_code, state_province=@state_province, city=@city, country_id=@country_id WHERE id = @id";

            try
            {
                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@street_address", streetAddress));
                command.Parameters.Add(new SqlParameter("@postal_code", postalCode));
                command.Parameters.Add(new SqlParameter("@state_province", stateProvince));
                command.Parameters.Add(new SqlParameter("@city", city));
                command.Parameters.Add(new SqlParameter("@country_id", countryId));

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
            command.CommandText = "DELETE FROM tbl_locations WHERE id = @id";

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
