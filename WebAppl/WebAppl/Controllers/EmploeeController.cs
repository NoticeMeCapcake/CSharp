using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebAppl.DbRepo;

namespace WebAppl.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase {
    private readonly IConfiguration _configuration;

    public EmployeeController(IConfiguration configuration) {
        _configuration = configuration;
        
    }

    [HttpGet]
    public JsonResult Get() {
        string query = "SELECT Employee.EmployeeId, Employee.EmployeeName, Department.DepartmentName, convert(varchar(10), DateOfJoining, 120) as DateOfJoining " +
                       "FROM Employee, Department WHERE Employee.DepartmentId = Department.DepartmentId";
        DataTable sqlDataTable = new DataTable();
        MySqlDataReader reader;
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                reader = cmd.ExecuteReader();
                sqlDataTable.Load(reader);
                reader.Close();
                conn.Close();
            }
        }
        Console.WriteLine("Hello World");
        Console.WriteLine(new JsonResult(sqlDataTable));
        return new JsonResult(sqlDataTable);
    } 
    [HttpPost]
    public JsonResult Post(Employee emp) {
        string query = @"INSERT INTO Employee (EmployeeName, DepartmentId, Age, Photo) 
                    VALUES (@EmployeeName, @DepartmentId, @Age, @Photo)";
        DataTable sqlDataTable = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@EmployeeId", (Object)emp.EmployeeId);
                cmd.Parameters.AddWithValue("@DepartmentId", emp.DepartmentId == null ? DBNull.Value : (Object)emp.DepartmentId);
                cmd.Parameters.AddWithValue("@Photo", emp.Photo);
                MySqlDataReader reader = cmd.ExecuteReader();
                sqlDataTable.Load(reader);
                reader.Close();
                conn.Close();
            }
        }
        Console.WriteLine("Hello World");
        Console.WriteLine(new JsonResult(sqlDataTable));
        return new JsonResult("Insertion success");
    }
    [HttpPut]
    public JsonResult Put(Employee emp) {
        string query = @"UPDATE Employee SET EmployeeName = @DepartmentName, DepartmentId = @DepartmentId, Age = @Age, Photo = @Photo 
                WHERE EmployeeId = @EmployeeId";
        DataTable sqlDataTable = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@EmployeeId", (Object)emp.EmployeeId);
                cmd.Parameters.AddWithValue("@DepartmentId", (Object)emp.DepartmentId);
                cmd.Parameters.AddWithValue("@Age", (Object)emp.Age);
                cmd.Parameters.AddWithValue("@Photo", emp.Photo);
                MySqlDataReader reader = cmd.ExecuteReader();
                sqlDataTable.Load(reader);
                reader.Close();
                conn.Close();
            }
        }
        Console.WriteLine("Hello World");
        Console.WriteLine(new JsonResult(sqlDataTable));
        return new JsonResult("Update success");
    }
    [HttpDelete("{id}")]
    public JsonResult Delete(int id) {
        string query = @"DELETE FROM Employee WHERE EmployeeId = @EmployeeId";
        DataTable sqlDataTable = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@EmployeeId", (Object) id);
                MySqlDataReader reader = cmd.ExecuteReader();
                sqlDataTable.Load(reader);
                reader.Close();
                conn.Close();
            }
        }
        Console.WriteLine("Hello World");
        Console.WriteLine(new JsonResult(sqlDataTable));
        return new JsonResult("Delete success");
    }
}