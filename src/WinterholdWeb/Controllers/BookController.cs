using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;
using WinterholdWeb.ViewModels;

namespace WinterholdWeb;

public class BookController : Controller
{
    private readonly BookService _service;

    public BookController(BookService service)
    {
        _service = service;
    }

    [HttpGet("book/detail/{categoryName}")]
    public IActionResult Index(string categoryName, int pageNumber = 1, string title = "", string author = "", bool isAvailable = false){
        ViewBag.CategoryName = categoryName;
        var viewModel = _service.GetByCategory(categoryName, pageNumber, title, author, isAvailable);
        return View(viewModel);
    }

    [HttpGet("book/detail/{categoryName}/upsert")]
    public IActionResult Add(string categoryName){
        ViewBag.CategoryName = categoryName;
        return View("Upsert", new BookUpsertViewModel(){
            Authors = _service.GetSelectListAuthors()
        });
    }

    [HttpPost("book/detail/{categoryName}/upsert")]
    public IActionResult Insert(string categoryName, BookUpsertViewModel viewModel){
        if(ModelState.IsValid){
            _service.Insert(categoryName, viewModel);
            return RedirectToAction("Index", new { categoryName = categoryName });
        }
        ViewBag.CategoryName = categoryName;
        return View("Upsert", new BookUpsertViewModel(){
            Authors = _service.GetSelectListAuthors()
        });
    }

    [HttpGet("book/detail/{categoryName}/upsert/{code}")]
    public IActionResult Edit(string categoryName, string code){
        var viewModel = _service.GetByCode(code);
        ViewBag.CategoryName = categoryName;
        return View("Upsert", viewModel);
    }

    [HttpPost("book/detail/{categoryName}/upsert/{code}")]
    public IActionResult Update(string categoryName, string code, BookUpsertViewModel viewModel){
        if(ModelState.IsValid){
            _service.Update(code, viewModel);
            return RedirectToAction("Index", new { categoryName = categoryName });
        }
        ViewBag.CategoryName = categoryName;
        viewModel.BookCode = code;
        viewModel.Authors = _service.GetSelectListAuthors();
        return View("Upsert", viewModel);
    }
}
