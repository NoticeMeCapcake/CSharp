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
        var query = @"SELECT Employee.EmployeeId, Employee.EmployeeName, Employee.Age, Department.DepartmentName, Employee.DateOfJoining, Employee.Photo
                       FROM Employee, Department WHERE Employee.DepartmentId = Department.DepartmentId";
        using var sqlDataTable = new DataTable();
        using (var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                var reader = cmd.ExecuteReader();
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
        var query = @"INSERT INTO Employee (EmployeeName, DepartmentId, Age, DateOfJoining, Photo) 
            VALUES (@EmployeeName, @DepartmentId, @Age, @DateOfJoining, @Photo)";

        using (var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (var cmd = new MySqlCommand(query, conn)) {
                var name = new MySqlParameter("@EmployeeName", MySqlDbType.VarChar, 500);
                var id = new MySqlParameter("@DepartmentId", MySqlDbType.Int32);
                var age = new MySqlParameter("@Age", MySqlDbType.Int32);
                var photo = new MySqlParameter("@Photo", MySqlDbType.Blob);
                var date = new MySqlParameter("@DateOfJoining", MySqlDbType.VarChar);
                name.Value = emp.EmployeeName;
                id.Value = (Object)emp.DepartmentId;
                age.Value = (Object)emp.Age;
                photo.Value = emp.Photo;
                date.Value = emp.DateOfJoining;
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(id);
                cmd.Parameters.Add(age);
                cmd.Parameters.Add(photo);
                cmd.Parameters.Add(date);
                cmd.Prepare();
                var reader = cmd.ExecuteNonQuery();

                conn.Close();
                
                // cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                // cmd.Parameters.AddWithValue("@DepartmentId", emp.DepartmentId == null ? DBNull.Value : (Object)emp.DepartmentId);
                // cmd.Parameters.AddWithValue("@Photo", emp.Photo);
                // cmd.Parameters.AddWithValue("@Age", (Object) emp.Age);
                // cmd.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                // Console.WriteLine(emp.DateOfJoining);
                // Console.WriteLine("sakhbas");
                // cmd.Prepare();
                // var reader = cmd.ExecuteReader();
                // sqlDataTable.Load(reader);
                // reader.Close();
                // conn.Close();
            }
        }
        return new JsonResult("Insertion success");
    }
    [HttpPut]
    public JsonResult Put(Employee emp) {
        string query = @"UPDATE Employee SET EmployeeName = @DepartmentName, DepartmentId = @DepartmentId, Age = @Age, Photo = @Photo 
                WHERE EmployeeId = @EmployeeId";
        using DataTable sqlDataTable = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                cmd.Parameters.AddWithValue("@EmployeeId", (Object)emp.EmployeeId);
                cmd.Parameters.AddWithValue("@DepartmentId", (Object)emp.DepartmentId);
                cmd.Parameters.AddWithValue("@Age", (Object)emp.Age);
                cmd.Parameters.AddWithValue("@Photo", emp.Photo);
                var reader = cmd.ExecuteReader();
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
        using DataTable sqlDataTable = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@EmployeeId", (Object) id);
                var reader = cmd.ExecuteReader();
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