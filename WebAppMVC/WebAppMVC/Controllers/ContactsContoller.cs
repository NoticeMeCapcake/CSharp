using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers;

public class Contacts : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}