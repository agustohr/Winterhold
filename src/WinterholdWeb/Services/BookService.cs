using Microsoft.AspNetCore.Mvc.Rendering;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels;
using static WinterholdWeb.Constants;

namespace WinterholdWeb.Services;

public class BookService
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    public BookService(IBookRepository repository, IAuthorRepository authorRepository)
    {
        _repository = repository;
        _authorRepository = authorRepository;
    }
    public BookIndexViewModel GetByCategory(string categoryName, int pageNumber, string title, string author, bool isAvailable){
        var model = _repository.Get(categoryName, pageNumber, PAGE_SIZE, title, author, isAvailable)
        .Select(book=>new BookViewModel(){
            Code = book.Code,
            Title = book.Title,
            CategoryName = book.CategoryName,
            AuthorName = book.Author.FirstName + " " + book.Author.LastName,
            LoanStatus = book.IsBorrowed ? "Borrowed" : "Available",
            Summary = book.Summary,
            ReleaseDate = book.ReleaseDate?.ToString("dd/MM/yyyy")??"",
            TotalPage = book.TotalPage
        });
        return new BookIndexViewModel(){
            Books = model.ToList(),
            Pagination = new PaginationViewModel(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountBooks(categoryName, title, author, isAvailable)
            },
            Title = title,
            Author = author,
            IsAvailable = isAvailable
        };
    }

    public List<SelectListItem> GetSelectListAuthors(){
        var selectListAuthors = _authorRepository.Get()
            .Select(author=>new SelectListItem(){
                Text = author.FirstName + " " + author.LastName,
                Value = author.Id.ToString()
            }).ToList();
        return selectListAuthors;
    }

    public BookUpsertViewModel GetByCode(string code){
        try{
            var model = _repository.GetByCode(code);
            return new BookUpsertViewModel(){
                BookCode = model.Code,
                Title = model.Title,
                AuthorId = model.AuthorId,
                Summary = model.Summary,
                ReleaseDate = model.ReleaseDate,
                TotalPage = model.TotalPage,
                Authors = GetSelectListAuthors()
            };
        }
        catch (Exception exception){
            throw new NullReferenceException(exception.Message);
        }
    }

    public void Insert(string categoryName, BookUpsertViewModel viewModel){
        var model = new Book(){
            Code = viewModel.BookCode,
            Title = viewModel.Title,
            CategoryName = categoryName,
            AuthorId = viewModel.AuthorId,
            IsBorrowed = false,
            Summary = viewModel.Summary,
            ReleaseDate = viewModel.ReleaseDate,
            TotalPage = viewModel.TotalPage
        };
        _repository.Insert(model);
    }

    public void Update(string code, BookUpsertViewModel viewModel){
        var model = _repository.GetByCode(code);
        model.Title = viewModel.Title;
        // model.CategoryName = categoryName;
        model.AuthorId = viewModel.AuthorId;
        model.Summary = viewModel.Summary;
        model.ReleaseDate = viewModel.ReleaseDate;
        model.TotalPage = viewModel.TotalPage;

        _repository.Update(model);
    }
}
