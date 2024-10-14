using Microsoft.AspNetCore.Mvc;
using WinterholdWeb.Services;
using WinterholdWeb.ViewModels;

namespace WinterholdWeb.Controllers;

public class CustomerController : Controller
{
    private readonly CustomerService _service;

    public CustomerController(CustomerService service)
    {
        _service = service;
    }
    
    [HttpGet("customer")]
    public IActionResult Index(int pageNumber = 1, string memberNumber = "", string fullName = "", bool isExpired = false){
        var viewModel = _service.Get(pageNumber, memberNumber, fullName, isExpired);
        return View(viewModel);
    }

    [HttpGet("customer/upsert")]
    public IActionResult Add(){
        return View("Upsert", new CustomerUpsertViewModel());
    }

    [HttpGet("customer/upsert/{memberNumber}")]
    public IActionResult Edit(string memberNumber){
        var viewModel = _service.GetByMemberNumber(memberNumber);
        return View("Upsert", viewModel);
    }

    [HttpPost("customer/upsert")]
    public IActionResult Add(CustomerUpsertViewModel viewModel){
        if(ModelState.IsValid){
            _service.Insert(viewModel);
            return RedirectToAction("Index");
        }
        return View("Upsert", new CustomerUpsertViewModel());
    }

    [HttpPost("customer/upsert/{memberNumber}")]
    public IActionResult Update(string memberNumber, CustomerUpsertViewModel viewModel){
        if(ModelState.IsValid){
            _service.Update(memberNumber, viewModel);
            return RedirectToAction("Index");
        }
        viewModel.MembershipNumber = memberNumber;
        return View("Upsert", viewModel);
    }
}
