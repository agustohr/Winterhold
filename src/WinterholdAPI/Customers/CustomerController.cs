using Microsoft.AspNetCore.Mvc;

namespace WinterholdAPI.Customers;

[Route("api/v1/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _service;

    public CustomerController(CustomerService service)
    {
        _service = service;
    }

    [HttpGet("{memberNumber}")]
    public IActionResult GetByName(string memberNumber){
        var dto = _service.GetByMemberNumber(memberNumber);
        return Ok(dto);
    }

    [HttpDelete("{memberNumber}")]
    public IActionResult Delete(string memberNumber){
        try{
            _service.Delete(memberNumber);
            return Ok($"Customer with member number {memberNumber} was deleted successfully");
        }catch(Exception exception){
            return StatusCode(500, $"Customer with member number {memberNumber} cannot be deleted because it has a relationship with another entity");
        }
    }

    [HttpPatch("extend/{memberNumber}")]
    public IActionResult Extend(string memberNumber){
        _service.ExtendExpiredDate(memberNumber);
        return Ok($"Expired date customer with member number {memberNumber} was extended successfully");
    }
}
