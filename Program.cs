

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


        var choice = true;
        while (choice)
        {
            Console.WriteLine("1. List all regions");
            Console.WriteLine("2. List all countries");
            Console.WriteLine("3. List all locations");
            Console.WriteLine("4. List all Departments");
            Console.WriteLine("5. List all Job");
            Console.WriteLine("6. List all Job History");
            Console.WriteLine("7. List all Employees");
            Console.WriteLine("8. List regions with Where");
            Console.WriteLine("9. Join tables regions and countries and locations");
            Console.WriteLine("10. List Detail Employees");
            Console.WriteLine("11. Total Employee in Department");
            Console.WriteLine("12. Exit");
            Console.Write("Enter your choice: ");
            var input = Console.ReadLine();
            choice = Menu(input);
        }
    }
    public static bool Menu(string input)
    {
        Region region;
        Country country;
        Location location;
        Department department;
        Job job;
        JobHistory jobHistory;
        Employee employee;

        List<Employee> getEmployee;
        List<Department> getDepartment;
        List<Location> getLocation;
        List<Country> getCountry;
        List<Region> getRegion;
        List<Job> getjob;
        List<JobHistory> getJobHistory; 

        switch (input)
        {
            case "1":
                region = new Region();
                var regions = region.GetAll();
                GeneralMenu.List(regions, "regions");
                break;
            case "2":
                country = new Country();
                var countries = country.GetAll();
                GeneralMenu.List(countries, "countries");
                break;
            case "3":
                location = new Location();
                var locations = location.GetAll();
                GeneralMenu.List(locations, "locations");
                break;
            case "4":
                department = new Department();
                var departments = department.GetAll();
                GeneralMenu.List(departments, "departments");
                break;
            case "5":
                job= new Job();
                var jobs = job.GetAll();
                GeneralMenu.List(jobs, "jobs");
                break;
            case "6":
                jobHistory = new JobHistory();
                var jobHistories = jobHistory.GetAll();
                GeneralMenu.List(jobHistories, "jobHistories");
                break;
            case "7":
                employee = new Employee();
                var employees = employee.GetAll();
                GeneralMenu.List(employees, "employees");
                break;
            case "8":
                var region2 = new Region();
                string input2 = Console.ReadLine();
                var result = region2.GetAll().Where(r => r.Name.Contains(input2)).ToList();
                GeneralMenu.List(result, "regions");
                break;
            case "9":
                var country3 = new Country();
                var region3 = new Region();
                var location3 = new Location();

                var getCountry2 = country3.GetAll();
                var getRegion2 = region3.GetAll();
                var getLocation2 = location3.GetAll();

                var resultJoin = (from r in getRegion2
                                  join c in getCountry2 on r.Id equals c.RegionId
                                  join l in getLocation2 on c.Id equals l.CountryId
                                  select new RegionAndCountryVM
                                  {
                                      CountryId = c.Id,
                                      CountryName = c.Name,
                                      RegionId = r.Id,
                                      RegionName = r.Name,
                                      City = l.City
                                  }).ToList();

                var resultJoin2 = getRegion2.Join(getCountry2,
                                                 r => r.Id,
                                                 c => c.RegionId,
                                                 (r, c) => new { r, c })
                                           .Join(getLocation2,
                                                 rc => rc.c.Id,
                                                 l => l.CountryId,
                                                 (rc, l) => new RegionAndCountryVM
                                                 {
                                                     CountryId = rc.c.Id,
                                                     CountryName = rc.c.Name,
                                                     RegionId = rc.r.Id,
                                                     RegionName = rc.r.Name,
                                                     City = l.City
                                                 }).ToList();
                GeneralMenu.List(resultJoin2, "regions and countries");
                break;

            case "10":
                employee = new Employee();
                department = new Department();
                location = new Location();
                country = new Country();
                region = new Region();

                getEmployee = employee.GetAll();
                getDepartment = department.GetAll();
                getLocation = location.GetAll();
                getCountry = country.GetAll();
                getRegion = region.GetAll();

                var resultEmployeeDetail = getRegion.Join(getCountry,
                                                 r => r.Id,
                                                 c => c.RegionId,
                                                 (r, c) => new { r, c })
                                           .Join(getLocation,
                                                 rc => rc.c.Id,
                                                 l => l.CountryId,
                                                 (rc, l) => new{rc, l})
                                           .Join(getDepartment, 
                                                 rcl => rcl.l.Id,
                                                 d => d.LocationId,
                                                 (rcl, d) => new {rcl, d})
                                           .Join(getEmployee,
                                                 rcld => rcld.d.Id,
                                                 e => e.DepartmentId,
                                                 (rcld, e) => new EmployeeDetailVM 
                                                            {
                                                                EmployeeId = e.Id,
                                                                FullName = $"{e.FirstName} {e.LastName}",
                                                                Email = e.Email,
                                                                Phone = e.PhoneNumber,
                                                                Salary = e.Salary,
                                                                DepartmentName = rcld.d.Name,
                                                                StreetAddress = rcld.rcl.l.StreetAddress,
                                                                CountryName = rcld.rcl.rc.c.Name,
                                                                RegionName = rcld.rcl.rc.r.Name
                                                            }).ToList();
                GeneralMenu.List(resultEmployeeDetail, "Detail Data Employee");

                break;
            case "11":
                department = new Department();
                employee = new Employee();

                getDepartment = department.GetAll();
                getEmployee = employee.GetAll();

                var resultEmployeeInDepartment = (from d in getDepartment
                                                  join e in getEmployee on d.Id equals e.DepartmentId 
                                                  group e by d.Name into g
                                                  where g.Count() > 3
                                                  select new EmployeeInDepartmentVM
                                                  {
                                                      TotalEmployee = g.Count(),
                                                      DepartmentName = g.Key,
                                                      MinSalary = g.Min(e => e.Salary),
                                                      MaxSalary = g.Max(e => e.Salary)
                                                  }).ToList();
                GeneralMenu.List(resultEmployeeInDepartment, "Total Employee In Department");
                /*var resultEmployeeInDepartment = GroupBygetDepartment.Join(getEmployee,
                                                                    d => d.Id,
                                                                    e => e.DepartmentId,
                                                                    (d, e) => new EmployeeInDepartmentVM
                                                                            {
                                                                                TotalEmployee = e.
                                                                            });*/

                break;
            case "12":
                return false;

            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        return true;
        //showData(new Employee());
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