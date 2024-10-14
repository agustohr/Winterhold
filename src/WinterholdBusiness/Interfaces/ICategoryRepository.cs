using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface ICategoryRepository
{
    public List<Category> Get();
    public List<Category> Get(int pageNumber, int pageSize, string? categoryName); 
    public int CountCategories(string? categoryName);
    public Category GetByName(string categoryName);
    public void Insert(Category category);
    public void Update(Category category);
    public void Delete(Category category);
}
