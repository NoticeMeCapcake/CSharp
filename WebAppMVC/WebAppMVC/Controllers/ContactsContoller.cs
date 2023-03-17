using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers;

public class Contacts : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Check(Contact contact)
    {
        if (ModelState.IsValid) {
            return Redirect("/");
        }
        else {
            return View("Index", contact);
        }
    }
}