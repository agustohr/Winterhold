using Microsoft.EntityFrameworkCore;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly WinterholdContext _dbContext;

    public AuthorRepository(WinterholdContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Author> Get(){
        return _dbContext.Authors.ToList();
    }

    public List<Author> Get(int pageNumber, int pageSize, string authorName)
    {
        return _dbContext.Authors
        .Where(author=>(author.FirstName + " " + author.LastName).ToLower().Contains(authorName??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }
    
    public int CountAuthors(string authorName)
    {
        return _dbContext.Authors
        .Where(author=>(author.FirstName + " " + author.LastName).ToLower().Contains(authorName??"".ToLower()))
        .Count();
    }

    public Author GetById(long id)
    {
        return _dbContext.Authors.Find(id) ?? throw new NullReferenceException($"Author with id {id} is not found");
    }
    public Author GetDetail(long id, int pageNumber, int pageSize)
    {
        return _dbContext.Authors
        .Include(author=>author.Books
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
        )
        .FirstOrDefault(author=>author.Id == id) ?? 
        throw new NullReferenceException($"Author with id {id} is not found");
    }

    public int CountDetail(long id)
    {
         return (_dbContext.Authors
        .Include(author=>author.Books)
        .FirstOrDefault(author=>author.Id == id))?
        .Books.Count()??0;
    }

    public void Insert(Author author)
    {
        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();
    }

    public void Update(Author author)
    {
        _dbContext.Authors.Update(author);
        _dbContext.SaveChanges();
    }

    public void Delete(Author author)
    {
        _dbContext.Authors.Remove(author);
        _dbContext.SaveChanges();
    }


}
