using WinterholdBusiness.Interfaces;
using WinterholdWeb.ViewModels;
using static WinterholdWeb.Constants;

namespace WinterholdWeb.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public CategoryIndexViewModel Get(int pageNumber, string categoryName){

        var model = _repository.Get(pageNumber, PAGE_SIZE, categoryName)
        .Select(category=>new CategoryViewModel(){
            Name = category.Name,
            Floor = category.Floor,
            Isle = category.Isle,
            Bay = category.Bay,
            TotalBooks = category.Books.Count()
        });

        return new CategoryIndexViewModel(){
            Categories = model.ToList(),
            Pagination = new PaginationViewModel(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountCategories(categoryName)
            },
            CategoryName = categoryName
        };
    }
}
