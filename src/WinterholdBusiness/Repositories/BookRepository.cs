using Microsoft.EntityFrameworkCore;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class BookRepository : IBookRepository
{
    private readonly WinterholdContext _dbContext;

    public BookRepository(WinterholdContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Book> Get(string? code)
    {
        return _dbContext.Books
        .Where(book=>!book.IsBorrowed || book.Code == code)
        .ToList();
    }

    public List<Book> Get(string categoryName, int pageNumber, int pageSize, string? title, string? author, bool isAvailable)
    {
        return _dbContext.Books
        .Include(book=>book.Author)
        .Where(book=>
            book.CategoryName == categoryName &&
            book.Title.ToLower().Contains(title??"".ToLower()) &&
            (book.Author.FirstName + " " + book.Author.LastName).ToLower().Contains(author??"".ToLower()) && 
            (isAvailable ? (book.IsBorrowed == false) : true)
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int CountBooks(string categoryName, string? title, string? author, bool isAvailable)
    {
        return _dbContext.Books
        .Include(book=>book.Author)
        .Where(book=>
            book.CategoryName == categoryName &&
            book.Title.ToLower().Contains(title??"".ToLower()) &&
            (book.Author.FirstName + " " + book.Author.LastName).ToLower().Contains(author??"".ToLower()) &&
            (isAvailable ? (book.IsBorrowed == false) : true)
        )
        .Count();
    }

    public Book GetByCode(string code)
    {
        return _dbContext.Books.Find(code) ?? throw new NullReferenceException($"Book with code {code} is not found");
    }

    public void Insert(Book book)
    {
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }

    public void Update(Book book)
    {
        _dbContext.Books.Update(book);
        _dbContext.SaveChanges();
    }

    public void Delete(Book book)
    {
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}
