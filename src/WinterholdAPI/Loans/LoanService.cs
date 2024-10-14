using Microsoft.AspNetCore.Mvc.Rendering;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdAPI.Loans;

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

    public List<SelectListItem> GetSelectListBooks(string? code){
        var selectListBooks = _bookRepository.Get(code)
            .Select(book=>new SelectListItem(){
                Text = book.Title,
                Value = book.Code
            }).ToList();
        return selectListBooks;
    }

    public List<SelectListItem> GetSelectListCustomers(){
        var selectListCustomers = _customerRepository.Get()
            .Select(customer=>new SelectListItem(){
                Text = customer.FirstName + " " + customer.LastName,
                Value = customer.MembershipNumber
            }).ToList();
        return selectListCustomers;
    }
    public LoanUpsertDto GetById(long id){
        var model = _repository.GetById(id);

        return new LoanUpsertDto(){
            Id = model.Id,
            BookCode = model.BookCode,
            CustomerNumber = model.CustomerNumber,
            LoanDate = model.LoanDate,
            Note = model.Note
        };
    }

    public LoanDetailDto GetDetailBookAndCustomer(long id){
        var model = _repository.GetById(id);

        return new LoanDetailDto(){
            Title = model.BookCodeNavigation.Title,
            Category = model.BookCodeNavigation.CategoryName,
            Author = model.BookCodeNavigation.Author.Title + ". " + model.BookCodeNavigation.Author.FirstName + " " + model.BookCodeNavigation.Author.LastName,
            Floor = model.BookCodeNavigation.CategoryNameNavigation.Floor,
            Isle = model.BookCodeNavigation.CategoryNameNavigation.Isle,
            Bay = model.BookCodeNavigation.CategoryNameNavigation.Bay,
            MembershipNumber = model.CustomerNumberNavigation.MembershipNumber,
            FullName = model.CustomerNumberNavigation.FirstName + " " + model.CustomerNumberNavigation.LastName,
            Phone = model.CustomerNumberNavigation.Phone,
            MembershipExpiredDate = model.CustomerNumberNavigation.MembershipExpireDate.ToString("dd/MM/yyyy")
        };
    }

    public void Insert(LoanUpsertDto loanDto){
        var model = new Loan(){
            CustomerNumber = loanDto.CustomerNumber,
            BookCode  = loanDto.BookCode,
            LoanDate = loanDto.LoanDate,
            Note = loanDto.Note,
            DueDate = loanDto.LoanDate.AddDays(5)
        };
        _repository.Insert(model);
        
        var book = _bookRepository.GetByCode(loanDto.BookCode);
        book.IsBorrowed = true;
        _bookRepository.Update(book);
    }

    public void Update(LoanUpsertDto loanDto){
        var model = _repository.GetById(loanDto.Id);
        
        if(loanDto.BookCode != model.BookCode){
            var book = _bookRepository.GetByCode(loanDto.BookCode);
            book.IsBorrowed = true;
            _bookRepository.Update(book);

            var oldBook = _bookRepository.GetByCode(model.BookCode);
            oldBook.IsBorrowed = false;
            _bookRepository.Update(book);
        }

        model.Id = loanDto.Id;
        model.CustomerNumber = loanDto.CustomerNumber;
        model.BookCode  = loanDto.BookCode;
        model.LoanDate = loanDto.LoanDate;
        model.Note = loanDto.Note;
        model.DueDate = loanDto.LoanDate.AddDays(5);

        _repository.Update(model);
    }

    public void ReturnBook(long id){
        var model = _repository.GetById(id);
        model.ReturnDate = DateTime.Now;
        _repository.Update(model);

        var book = _bookRepository.GetByCode(model.BookCodeNavigation.Code);
        book.IsBorrowed = false;
        _bookRepository.Update(book);
    }
}
