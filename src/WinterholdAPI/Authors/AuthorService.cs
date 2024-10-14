using WinterholdBusiness.Interfaces;

namespace WinterholdAPI.Authors;

public class AuthorService
{
    private readonly IAuthorRepository _repository;
    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public void Delete(long id){
        var model = _repository.GetById(id);
        _repository.Delete(model);
    }
}
