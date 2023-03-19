using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebAppl.DbRepo;

namespace WebAppl.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase {
    private readonly IConfiguration _configuration;

    public DepartmentController(IConfiguration configuration) {
        _configuration = configuration;
        
    }

    [HttpGet]
    public JsonResult Get() {
        string query = "SELECT * FROM Department";
        DataTable sqlDataTable = new DataTable();
        MySqlDataReader reader;
        Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));
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
    public JsonResult Post(Department dep) {
        string query = @"INSERT INTO Department (DepartmentName) VALUES (@DepartmentName)";
        DataTable sqlDataTable = new DataTable();
        Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
    public JsonResult Put(Department dep) {
        string query = @"UPDATE Department SET DepartmentName = @DepartmentName 
                  WHERE DepartmentId = @DepartmentId";
        DataTable sqlDataTable = new DataTable();
        Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@DepartmentId", (Object)dep.DepartmentId);
                cmd.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
        string query = @"DELETE FROM Department 
                  WHERE DepartmentId = @DepartmentId";
        DataTable sqlDataTable = new DataTable();
        Console.WriteLine(_configuration.GetConnectionString("DefaultConnection"));
        using (MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@DepartmentId", (Object) id);
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