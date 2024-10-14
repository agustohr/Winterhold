using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface IBookRepository
{
    // public List<Book> Get(); 
    public List<Book> Get(string? code);
    public List<Book> Get(string categoryName, int pageNumber, int pageSize, string? title, string? author, bool isAvailable); 
    public int CountBooks(string categoryName, string? title, string? author, bool isAvailable);
    public Book GetByCode(string code);
    public void Insert(Book book);
    public void Update(Book book);
    public void Delete(Book book);
}
