using Microsoft.AspNetCore.Mvc;

namespace WebAppWithDockerAndReact.Controllers; 

[Route("api/Users")]
[ApiController]
public class UserController : ControllerBase {
    // GET
    [HttpGet]
    public IActionResult GetUsers() {
        var users = new[] {
            new { Id = 1, Name = "John" },
            new { Id = 2, Name = "Jane" },
            new { Id = 3, Name = "Bob" }
        };
        return Ok(users);
    }
}