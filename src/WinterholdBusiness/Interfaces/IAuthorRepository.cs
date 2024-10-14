using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface IAuthorRepository
{
    public List<Author> Get();
    public List<Author> Get(int pageNumber, int pageSize, string authorName); 
    public int CountAuthors(string authorName);
    public Author GetById(long id);
    public Author GetDetail(long id,int pageNumber, int pageSize);
    public int CountDetail(long id);
    public void Insert(Author author);
    public void Update(Author author);
    public void Delete(Author author);
}
