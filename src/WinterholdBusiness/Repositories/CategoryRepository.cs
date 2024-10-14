using Microsoft.EntityFrameworkCore;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly WinterholdContext _dbContext;

    public CategoryRepository(WinterholdContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Category> Get()
    {
        var categories = _dbContext.Categories;
        return categories.ToList();
    }

    public List<Category> Get(int pageNumber, int pageSize, string? categoryName)
    {
        return _dbContext.Categories
        .Where(category=>category.Name.ToLower().Contains(categoryName??"".ToLower()))
        .Include(category=>category.Books)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int CountCategories(string? categoryName)
    {
        return _dbContext.Categories
        .Where(category=>category.Name.ToLower().Contains(categoryName??"".ToLower()))
        .Count();
    }

    public Category GetByName(string categoryName)
    {
        return _dbContext.Categories.Find(categoryName) ?? throw new NullReferenceException($"Category with name {categoryName} is not found");
    }

    public void Insert(Category category)
    {
        _dbContext.Categories.Add(category);
        _dbContext.SaveChanges();
    }

    public void Update(Category category)
    {
        _dbContext.Categories.Update(category);
        _dbContext.SaveChanges();
    }

    public void Delete(Category category)
    {
        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();
    }
}
