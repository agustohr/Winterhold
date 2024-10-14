using Microsoft.AspNetCore.Mvc;

namespace WinterholdAPI.Books;

[Route("api/v1/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookService _service;

    public BookController(BookService bookService)
    {
        _service = bookService;
    }

    [HttpDelete("{code}")]
    public IActionResult Delete(string code){
        try{
            _service.Delete(code);
            return Ok($"Book with code {code} was deleted successfully");
        }catch(Exception exception){
            return StatusCode(500, $"Book with code {code} cannot be deleted because it has a relationship with another entity");
        }
    }
}
