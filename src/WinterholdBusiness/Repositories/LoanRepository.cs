using Microsoft.EntityFrameworkCore;
using WinterholdBusiness.Interfaces;
using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly WinterholdContext _dbContext;

    public LoanRepository(WinterholdContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Loan> Get(int pageNumber, int pageSize, string? bookTitle, string? customerName, bool isPassed)
    {
        return _dbContext.Loans
        .Include(loan=>loan.BookCodeNavigation)
        .Include(loan=>loan.CustomerNumberNavigation)
        .Where(loan=>
            loan.BookCodeNavigation.Title.ToLower().Contains(bookTitle??"".ToLower()) &&
            (loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName).ToLower().Contains(customerName??"".ToLower()) && 
            (isPassed ? (loan.DueDate < DateTime.Now) : true)
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }
    
    public int CountLoans(string? bookTitle, string? customerName, bool isPassed)
    {
        return _dbContext.Loans
        .Include(loan=>loan.BookCodeNavigation)
        .Include(loan=>loan.CustomerNumberNavigation)
        .Where(loan=>
            loan.BookCodeNavigation.Title.ToLower().Contains(bookTitle??"".ToLower()) &&
            (loan.CustomerNumberNavigation.FirstName + " " + loan.CustomerNumberNavigation.LastName).ToLower().Contains(customerName??"".ToLower()) && 
            (isPassed ? (loan.DueDate < DateTime.Now) : true)
        )
        .Count();
    }

    public Loan GetById(long id)
    {
        return _dbContext.Loans
        .Include(loan=>loan.BookCodeNavigation)
            .ThenInclude(book=>book.Author)
        .Include(loan=>loan.BookCodeNavigation)
            .ThenInclude(book=>book.CategoryNameNavigation)
        .Include(loan=>loan.CustomerNumberNavigation)
        .FirstOrDefault(loan=>loan.Id == id) ??
        throw new NullReferenceException($"Loan with id {id} is not found");
    }

    public void Insert(Loan loan)
    {
        _dbContext.Loans.Add(loan);
        _dbContext.SaveChanges();
    }

    public void Update(Loan loan)
    {
        _dbContext.Loans.Update(loan);
        _dbContext.SaveChanges();
    }

    public void Delete(Loan loan)
    {
        _dbContext.Loans.Remove(loan);
        _dbContext.SaveChanges();
    }
}
