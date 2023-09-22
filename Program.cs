using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using BasicConnectivity.Controllers;
using BasicConnectivity.Model;
using BasicConnectivity.ViewModel;
using BasicConnectivity.Views;

namespace BasicConnectivity;

public class Program
{
    private static void Main(string[] args)
    {
        var choice = true;
        while (choice)
        {
            Console.WriteLine("1. Regions");
            Console.WriteLine("2. Countries");
            Console.WriteLine("3. Locations");
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
                RegionMenu();
                break;
            case "2":
                CountryMenu();
                break;
            case "3":
                LocationMenu();
                break;
            case "4":
                department = new Department();
                var departments = department.GetAll();
                break;
            case "5":
                job = new Job();
                var jobs = job.GetAll();
                break;
            case "6":
                jobHistory = new JobHistory();
                var jobHistories = jobHistory.GetAll();
                break;
            case "7":
                employee = new Employee();
                var employees = employee.GetAll();
                break;
            case "8":
                var region2 = new Region();
                string input2 = Console.ReadLine();
                var result = region2.GetAll().Where(r => r.Name.Contains(input2)).ToList();
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

                //Method Syntax
                var resultEmployeeDetail = getRegion.Join(getCountry,
                                                 r => r.Id,
                                                 c => c.RegionId,
                                                 (r, c) => new { r, c })
                                           .Join(getLocation,
                                                 rc => rc.c.Id,
                                                 l => l.CountryId,
                                                 (rc, l) => new { rc, l })
                                           .Join(getDepartment,
                                                 rcl => rcl.l.Id,
                                                 d => d.LocationId,
                                                 (rcl, d) => new { rcl, d })
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

                break;
            case "11":
                department = new Department();
                employee = new Employee();

                getDepartment = department.GetAll();
                getEmployee = employee.GetAll();

                //Query Syntax
                var resultEmployeeInDepartment = (from d in getDepartment
                                                  join e in getEmployee on d.Id equals e.DepartmentId
                                                  group e by d.Name into g
                                                  where g.Count() > 3
                                                  select new EmployeeInDepartmentVM
                                                  {
                                                      TotalEmployee = g.Count(),
                                                      DepartmentName = g.Key,
                                                      MinSalary = g.Min(e => e.Salary),
                                                      MaxSalary = g.Max(e => e.Salary),
                                                      AvgSalary = g.Average(e => e.Salary)
                                                  }).ToList();

                /*  var resultEmployeeInDepartment2 = getDepartment.Join(getEmployee, 
                                                                      d => d.Id, 
                                                                      e => e.DepartmentId, 
                                                                      (d, e) => new {d, e})
                                                                          .GroupBy(de => de.d.Name)
                                                                          .Where(g => g.Count() > 3)
                                                                          .Select(g => new EmployeeInDepartmentVM
                                                                                      {
                                                                                          TotalEmployee = g.Count(),
                                                                                          DepartmentName = g.Key,
                                                                                          MinSalary = g.Min(e => e.e.Salary),
                                                                                          MaxSalary = g.Max(e => e.e.Salary)
                                                                                      })
                                                                                      .ToList();
                  GeneralMenu.List(resultEmployeeInDepartment2, "Total Employee In Department");*/
                break;
            case "12":
                return false;

            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        return true;

    }

    public static void RegionMenu()
    {
        var region = new Region();
        var regionView = new RegionView();

        var regionController = new RegionController(region, regionView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all regions");
            Console.WriteLine("2. Insert new region");
            Console.WriteLine("3. Update region");
            Console.WriteLine("4. Delete region");
            Console.WriteLine("5. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    regionController.GetAll();
                    break;
                case "2":
                    regionController.Insert();
                    break;
                case "3":
                    regionController.Update();
                    break;                
                case "4":
                    regionController.Delete();
                    break;
                case "5":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    public static void CountryMenu()
    {
        var country = new Country();
        var countryView = new CountryView();

        var countryController = new CountryController(country, countryView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all countries");
            Console.WriteLine("2. Insert new country");
            Console.WriteLine("3. Update country");
            Console.WriteLine("4. Delete country");
            Console.WriteLine("5. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    countryController.GetAll();
                    break;
                case "2":
                    countryController.Insert();
                    break;
                case "3":
                    countryController.Update();
                    break;
                case "4":
                    countryController.Delete();
                    break;
                case "5":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    public static void LocationMenu()
    {
        var location = new Location();
        var locationView = new LocationView();

        var locationController = new LocationController(location, locationView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all locations");
            Console.WriteLine("2. Insert new location");
            Console.WriteLine("3. Update location");
            Console.WriteLine("4. Delete location");
            Console.WriteLine("5. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    locationController.GetAll();
                    break;
                case "2":
                    locationController.Insert();
                    break;
                case "3":
                    locationController.Update();
                    break;
                case "4":
                    locationController.Delete();
                    break;
                case "5":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    /*    public static void AllMenu<T>(T model, T view, T controller)
        {
            *//*var models = new T();
            var regionView = new RegionView();

            var controller = new RegionController(region, regionView);*//*

            var isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("1. List all regions");
                Console.WriteLine("2. Insert new region");
                Console.WriteLine("3. Update region");
                Console.WriteLine("4. Delete region");
                Console.WriteLine("10. Back");
                Console.Write("Enter your choice: ");
                var input2 = Console.ReadLine();
                switch (input2)
                {
                    case "1":
                        controller.GetAll();
                        break;
                    case "2":
                        regionController.Insert();
                        break;
                    case "3":
                        regionController.Update();
                        break;
                    case "4":
                        regionController.Delete();
                        break;
                    case "10":
                        isLoop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
}
}*/
}