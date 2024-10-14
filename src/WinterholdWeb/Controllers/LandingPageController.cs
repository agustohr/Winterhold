using Microsoft.AspNetCore.Mvc;

namespace WinterholdWeb.Controllers;

public class LandingPageController : Controller
{
    public IActionResult Index(){
        return View();
    }
}
