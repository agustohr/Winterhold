using Microsoft.AspNetCore.Mvc;

namespace WinterholdAPI.Loans;

[Route("api/v1/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly LoanService _service;

    public LoanController(LoanService service)
    {
        _service = service;
    }

    [HttpGet("selectlist/{code}")]
    public IActionResult GetSelectList(string? code){
        return Ok(new { 
            SelectListCustomer = _service.GetSelectListCustomers(),
            SelectListBook = _service.GetSelectListBooks(code)
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(long id){
        var dto = _service.GetById(id);
        return Ok(dto);
    }

    [HttpGet("detail/{id}")]
    public IActionResult GetDetail(long id){
        var dto = _service.GetDetailBookAndCustomer(id);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Add(LoanUpsertDto loanDto){
        if(loanDto.BookCode != null && loanDto.CustomerNumber != null && loanDto.LoanDate != new DateTime()){
            _service.Insert(loanDto);
            return Created("", loanDto);
        }
        return BadRequest("There is data that has not been filled!");
    }

    [HttpPut("{id}")]
    public IActionResult Edit(long id, LoanUpsertDto loanDto){
        if(loanDto.BookCode != null && loanDto.CustomerNumber != null && loanDto.LoanDate != new DateTime()){
            loanDto.Id = id;
            _service.Update(loanDto);
            return Ok(loanDto);
        }
        return BadRequest("There is data that has not been filled!");
    }

    [HttpPatch("return/{id}")]
    public IActionResult Return(long id){
        _service.ReturnBook(id);
        return Ok("The borrowed book was successfully returned");
    }
}
