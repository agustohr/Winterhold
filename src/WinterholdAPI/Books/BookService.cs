using WinterholdBusiness.Interfaces;

namespace WinterholdAPI.Books;

public class BookService
{
    private readonly IBookRepository _repository;
    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public void Delete(string code){
        var model = _repository.GetByCode(code);
        _repository.Delete(model);
    }
}
