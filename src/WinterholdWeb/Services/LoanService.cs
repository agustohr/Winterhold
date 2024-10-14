using WinterholdBusiness.Interfaces;
using WinterholdWeb.ViewModels;
using static WinterholdWeb.Constants;

namespace WinterholdWeb.Services;

public class LoanService
{
    private readonly ILoanRepository _repository;
    private readonly IBookRepository _bookRepository;
    private readonly ICustomerRepository _customerRepository;
    public LoanService(ILoanRepository repository, IBookRepository bookRepository, ICustomerRepository customerRepository)
    {
        _repository = repository;
        _bookRepository = bookRepository;
        _customerRepository = customerRepository;
    }

    public LoanIndexViewModal Get(int pageNumber, string bookTitle, string customerName, bool isPassed){
        var model = _repository.Get(pageNumber, PAGE_SIZE, bookTitle, customerName, isPassed)
        .Select(loan=>new LoanViewModel(){
            Id = loan.Id,
            BookTitle = loan.BookCodeNavigation.Title,
            CustomerName = loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName,
            LoanDate = loan.LoanDate.ToString("dd/MM/yyyy"),
            DueDate = loan.DueDate.ToString("dd/MM/yyyy"),
            ReturnDate = loan.ReturnDate?.ToString("dd/MM/yyyy")??""
        });
        return new LoanIndexViewModal(){
            Loans = model.ToList(),
            Pagination = new PaginationViewModel(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountLoans(bookTitle, customerName, isPassed)
            },
            BookTitle = bookTitle,
            CustomerName = customerName,
            IsPassed = isPassed
        };
    }
}
