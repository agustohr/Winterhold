using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdAPI.Categories;

public class CategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public CategoryDto GetByName(string categoryName){
        var model = _repository.GetByName(categoryName);

        return new CategoryDto(){
            Name = model.Name,
            Floor = model.Floor,
            Isle = model.Isle,
            Bay = model.Bay
        };
    }

    public void Insert(CategoryDto categoryDto){
        var model = new Category(){
            Name = categoryDto.Name,
            Floor = categoryDto.Floor,
            Isle = categoryDto.Isle,
            Bay = categoryDto.Bay
        };
        _repository.Insert(model);
    }

    public void Update(string categoryName, CategoryDto categoryDto){
        var model = new Category(){
            Name = categoryName,
            Floor = categoryDto.Floor,
            Isle = categoryDto.Isle,
            Bay = categoryDto.Bay
        };
        _repository.Update(model);
    }

    public void Delete(string categoryName){
        var model = _repository.GetByName(categoryName);
        _repository.Delete(model);
    }
}
