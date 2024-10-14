using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;

namespace WinterholdWeb.Controllers;

public class LoanController : Controller
{
    private readonly LoanService _service;

    public LoanController(LoanService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IActionResult Index(int pageNumber = 1, string bookTitle = "", string customerName = "", bool isPassed = false){
        var viewModal = _service.Get(pageNumber, bookTitle, customerName, isPassed);
        return View(viewModal);
    }
}
