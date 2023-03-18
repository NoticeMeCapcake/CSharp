using System.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace WebAppReact.Controllers;

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
        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader()) {
                sqlDataTable.Load(reader);
                reader.Close();
            }
            conn.Close();
        }
        
        Console.WriteLine("Hello World");
        Console.WriteLine(new JsonResult(sqlDataTable));

        return new JsonResult(sqlDataTable);
    }
}