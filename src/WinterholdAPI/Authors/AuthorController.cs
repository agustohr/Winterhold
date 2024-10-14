using Microsoft.AspNetCore.Mvc;

namespace WinterholdAPI.Authors;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _service;

    public AuthorController(AuthorService bookService)
    {
        _service = bookService;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id){
        try{
            _service.Delete(id);
            return Ok($"Author with id {id} was deleted successfully");
        }catch(Exception exception){
            return StatusCode(500, $"Author with id {id} cannot be deleted because it has a relationship with another entity");
        }
    }
}
