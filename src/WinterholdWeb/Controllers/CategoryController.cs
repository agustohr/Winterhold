using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;

namespace WinterholdWeb.Controllers;

public class CategoryController : Controller
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet("book/category")]
    public IActionResult Index(int pageNumber = 1, string categoryName = ""){
        var viewModel = _service.Get(pageNumber, categoryName);
        return View(viewModel);
    }

    [HttpGet("book/category/{categoryName}")]
    public IActionResult Books(string categoryName){
        return RedirectToAction("Index", "Book", new { categoryName = categoryName });
    }
}
