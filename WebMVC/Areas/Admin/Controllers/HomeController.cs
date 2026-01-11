using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Areas.Admin.Controllers;

public class HomeController : AdminController
{
    public IActionResult Index()
    {
        return View();
    }
}
