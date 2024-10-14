using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;
using WinterholdWeb.ViewModels;
using static WinterholdWeb.Constants;

namespace WinterholdWeb.Services;

public class AuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(IAuthorRepository repository)
    {
        _repository = repository;
    }

    private static int SetAge(DateTime birthDate){
        TimeSpan duration = DateTime.Now - birthDate;
        return (Int32)duration.TotalDays/365;
    }

    public AuthorIndexViewModel Get(int pageNumber, string authorName){
        var model = _repository.Get(pageNumber, PAGE_SIZE, authorName)
            .Select(author => new AuthorViewModel(){
                Id = author.Id,
                FullName = $"{author.Title}. {author.FirstName} {author.LastName}",
                Age = SetAge(author.BirthDate),
                IsDeceased = author.DeceasedDate != null,
                Education = author.Education??"No Information"
            });

        return new AuthorIndexViewModel(){
            Authors = model.ToList(),
            Pagination = new PaginationViewModel(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountAuthors(authorName)
            },
            AuthorName = authorName,
        };
    }

    public AuthorUpsertViewModel Get(long id){
        try
        {
            var model = _repository.GetById(id);
            return new AuthorUpsertViewModel(){
                Id = model.Id,
                Title = model.Title,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                DeceasedDate = model.DeceasedDate,
                Education = model.Education,
                Summary = model.Summary
            };
        }
        catch (Exception exception)
        {
            throw new NullReferenceException(exception.Message);
        }
    }

    public void Insert(AuthorUpsertViewModel viewModel){
        var model = new Author(){
            Title = viewModel.Title,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            BirthDate = viewModel.BirthDate?? new DateTime(),
            DeceasedDate = viewModel.DeceasedDate,
            Education = viewModel.Education,
            Summary = viewModel.Summary
        };
        _repository.Insert(model);
    }

    public void Update(AuthorUpsertViewModel viewModel){
        var model = new Author(){
            Id = viewModel.Id,
            Title = viewModel.Title,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            BirthDate = viewModel.BirthDate?? new DateTime(),
            DeceasedDate = viewModel.DeceasedDate,
            Education = viewModel.Education,
            Summary = viewModel.Summary
        };
        _repository.Update(model);
    }

    public void Delete(long id){
        var model = _repository.GetById(id);
        _repository.Delete(model);
    }

    public AuthorDetailViewModel GetDetail(long id, int pageNumber){
        var model = _repository.GetDetail(id, pageNumber, PAGE_SIZE);
        var books = model.Books
        .Select(book=>new BookViewModel(){
            Title = book.Title,
            CategoryName = book.CategoryName,
            LoanStatus = book.IsBorrowed ? "Borrowed" : "Available",
            ReleaseDate = book.ReleaseDate?.ToString("dd/MM/yyyy")??"",
            TotalPage = book.TotalPage
        });

        return new AuthorDetailViewModel(){
            Id = model.Id,
            FullName = $"{model.Title}. {model.FirstName} {model.LastName}",
            BirthDate = model.BirthDate.ToString("dd MMMM yyyy"),
            DeceasedDate = model.DeceasedDate?.ToString("dd MMMM yyyy")??"-",
            Education = model.Education??"No Information",
            Summary = model.Summary,
            Books = books.ToList(),
            PaginationBooks = new PaginationViewModel(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountDetail(id)
            }
        };
    }
}
