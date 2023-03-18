using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using MySqlConnector;

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
}