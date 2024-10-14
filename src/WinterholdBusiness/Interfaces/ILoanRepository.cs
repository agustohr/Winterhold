using WinterholdDataAccess.Models;

namespace WinterholdBusiness.Interfaces;

public interface ILoanRepository
{
    public List<Loan> Get(int pageNumber, int pageSize, string? bookTitle, string? customerName, bool isPassed); 
    public int CountLoans(string? bookTitle, string? customerName, bool isPassed);
    public Loan GetById(long id);
    public void Insert(Loan loan);
    public void Update(Loan loan);
    public void Delete(Loan loan);
}
