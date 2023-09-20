

/*
 * Tidak dianjurkan menulis query dengan String interpolation krn rawan Sql injection (Kemanan db)
 * setiap melakukan query lakukan try catch
 * Saat mengunakan query db atur penggunaan memory nya. ada garbage collection 
 * Di c# tinggal menggunakan using, Dibutuhkan saat mengambil resource dr luar(ex database)
 * Kalo variabel biasa ata object tidak perlu menggunakan using
 * Ketika class menggunakan IDisposible maka wajib menggunakan using (klik kanan - go to definition
 * Penggunaan using adalah saat instansiasi
 */

using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace BasicConnectivity;
/*
 * Nuget utk library - ada di tools
 * Pastikan package source nya nuget.org
 * Library di C# namanya Provider

*/
public class Program
{
    /*
        Integrated Security - Windows Authentication
        Di VS bisa liat database (Data Processing Saat install) - Sql Server Object Explorer
     */
    public static readonly string connectionString = "Data Source=DESKTOP-2A0I62H; Database=db_hr; Connect Timeout=; Integrated Security=True";
    //static SqlConnection connection;

    private static void DbConnection()
    {

    }
    private static void Main(String[] args)
    {
        /*
         Yang sering terjadi tidak error tpi progrm gk jalan, biasanya ada typo di connectionstring
         */

        //GetAllRegions();
        Insert(" ");
        InsertRegion("East Europe asjdkajskdjsakjdkajskdjskdjkajasdjaskjdhjahkhjhkjhkhkjhjkhkhkhjhjhkhkhkjhhkjhkjhkjhkj");
        //GetAllRegions();
        /*      GetRegionById(12);
              UpdateRegion(12, "East Asia");
              GetRegionById(12);
              GetAllRegions();
              DeleteRegion(9);*/
        //InsertRegion("Balkan");
        //Delete(10);
        GetAllRegions();

    }
    //GET ALL: regions
    public static void GetAllRegions()
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_regions";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    Console.WriteLine($"Id: {reader.GetInt32(0)}");
                    Console.WriteLine($"Name: {reader.GetString(1)}");
                }
            else
                Console.WriteLine("No Rows Found");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errod: {ex.Message}");
        }
    }
    //GET BY ID: regions
    public static void GetRegionById(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

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
                while (reader.Read())
                {
                    Console.WriteLine($"Id: {reader.GetInt32(0)}");
                    Console.WriteLine($"Name: {reader.GetString(1)}");
                }
            else
                Console.WriteLine("No Rows Found");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    //INSERT: regions
    public static void InsertRegion(string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

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

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Insert Success");
                        break;
                    default:
                        Console.WriteLine("Insert Failed");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    //UPDATE: regions
    public static void UpdateRegion(int id, string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

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

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Update Success");
                        break;
                    default:
                        Console.WriteLine("Update Failed");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    //DELETE: regions
    public static void DeleteRegion(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

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

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Delete Success");
                        break;
                    default:
                        Console.WriteLine("Delete Failed");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();
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
            int rowDeleted = command.ExecuteNonQuery();
            if (rowDeleted > 0)
                Console.WriteLine("Row Deleted");
            else
                Console.WriteLine("Delete Failed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void Insert(string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();
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

            var result = command.ExecuteNonQuery();
            connection.Close();

            switch (result)
            {
                case >= 1:
                    Console.WriteLine("Insert Success");
                    break;
                default:
                    Console.WriteLine("Insert Failed");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void InsertCountry(int id, string name, int region_id)
    {
        using var connection = new SqlConnection();
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO tbl_countries VALUES (@id, @name, @region_id)";

        try
        {
            var pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.Value = name;
            pName.SqlDbType = SqlDbType.VarChar;
            command.Parameters.Add(pName);

            connection.Open();

            var result = command.ExecuteNonQuery();
            switch (result)
            {
                case >0 :
                    Console.WriteLine($"Insert {name} into countries table Successed ");
                    break;
                default:
                    Console.WriteLine("Failed");
                    break;
            }
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}