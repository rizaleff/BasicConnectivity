

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

    private static void Menu()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("1. Regions ");
        Console.WriteLine("2. Countries ");
        Console.WriteLine("3. Location");
        Console.WriteLine("4. Department");
        Console.WriteLine("5. Job");
        Console.WriteLine("6. Job History");
        Console.WriteLine("7. Employee");
        Console.Write("Pilih Menu: ");
        Console.ReadLine();
    }

    private static void SubMenu()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("1. Get All");
        Console.WriteLine("2. Get By Id");
        Console.WriteLine("3. Insert ");
        Console.WriteLine("4. Update ");
        Console.WriteLine("5. Delete ");
    }
    private static void showData<T>(T data) where T : Table<T>
    {

        var getAllData = data.GetAll();

        if (getAllData.Count > 0)
        {
            foreach (var data1 in getAllData)
            {
                data1.Print();
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }

    }
    private static void Main(String[] args)
    {

        showData(new Employee());
/*        var region = new Region();
        //region.Delete(24);
        region.Update(21, "Fake Region");
        var getAllRegion = region.GetAll();

        if (getAllRegion.Count > 0)
        {
            foreach (var region1 in getAllRegion)
            {
                Console.WriteLine($"Id: {region1.Id}, Name: {region1.Name}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }
*/
       /* var insertResult = region.Insert("Region 5");
        int.TryParse(insertResult, out int result);
        if (result > 0)
        {
            Console.WriteLine("Insert Success");
        }
        else
        {
            Console.WriteLine("Insert Failed");
            Console.WriteLine(insertResult);
        }


        getAllRegion = region.GetAll();

        if (getAllRegion.Count > 0)
        {
            foreach (var region1 in getAllRegion)
            {
                Console.WriteLine($"Id: {region1.Id}, Name: {region1.Name}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }*/

    }

/*    //Insert Country
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

    //get country
    public static void GetAllCountries()
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_countries";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    Console.WriteLine($"Id: {reader.GetInt32(0)}");
                    Console.WriteLine($"Name: {reader.GetString(1)}");
                    Console.WriteLine($"Id Region: {reader.GetString(2)}");
                }
            else
                Console.WriteLine("No Rows Found");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errod: {ex.Message}");
        }
    }*/
}