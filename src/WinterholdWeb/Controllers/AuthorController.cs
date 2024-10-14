using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;
using WinterholdWeb.ViewModels;

namespace WinterholdWeb.Controllers;

public class AuthorController : Controller
{
    private readonly AuthorService _service;

    public AuthorController(AuthorService service)
    {
        _service = service;
    }

    [HttpGet("author")]
    public IActionResult Index(int pageNumber = 1, string authorName = ""){
        var viewModel = _service.Get(pageNumber, authorName);
        return View(viewModel);
    }

    [HttpGet("author/upsert")]
    public IActionResult Add(){
        return View("Upsert", new AuthorUpsertViewModel());
    }

    [HttpGet("author/upsert/{id}")]
    public IActionResult Edit(long id){
        var viewModel = _service.Get(id);
        return View("Upsert", viewModel);
    }

    [HttpPost("author/upsert")]
    public IActionResult Insert(AuthorUpsertViewModel viewModel){
        if(ModelState.IsValid){
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        return View("Upsert", new AuthorUpsertViewModel());
    }

    [HttpPost("author/upsert/{id}")]
    public IActionResult Update(AuthorUpsertViewModel viewModel){
        if(ModelState.IsValid){
            _service.Update(viewModel);
            return RedirectToAction("Index");
        }
        return View("Upsert", new AuthorUpsertViewModel());
    }

    [HttpGet("author/delete/{id}")]
    public IActionResult Delete(long id){
        _service.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet("author/{id}/detail")]
    public IActionResult Detail(long id, int pageNumber = 1){
        var viewModel = _service.GetDetail(id, pageNumber);
        return View("Detail", viewModel);
    }

}
