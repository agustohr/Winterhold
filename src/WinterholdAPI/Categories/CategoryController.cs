using Microsoft.AspNetCore.Mvc;

namespace WinterholdAPI.Categories;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    [HttpGet("{categoryName}")]
    public IActionResult GetByName(string categoryName){
        var dto = _service.GetByName(categoryName);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult Add(CategoryDto categoryDto){
        try{
            if(categoryDto.Name != "" && categoryDto.Floor != 0 && categoryDto.Isle != "" && categoryDto.Bay != ""){
                _service.Insert(categoryDto);
                return Created("", categoryDto);
            }
            return BadRequest("There is data that has not been filled!");
        }catch(Exception exception){
            return Conflict("Category Name is Exist!");
        }
    }

    [HttpPut("{categoryName}")]
    public IActionResult Edit(string categoryName, CategoryDto categoryDto){
        if(categoryDto.Name != "" && categoryDto.Floor != 0 && categoryDto.Isle != "" && categoryDto.Bay != ""){
            _service.Update(categoryName, categoryDto);
            return Ok(categoryDto);
        }
        return BadRequest("There is data that has not been filled!");
    }

    [HttpDelete("{categoryName}")]
    public IActionResult Delete(string categoryName){
        try{
            _service.Delete(categoryName);
            return Ok($"Category with name {categoryName} was deleted successfully");
        }catch(Exception exception){
            return StatusCode(500, $"Category {categoryName} cannot be deleted because it has a relationship with another entity");
        }
    }
}
