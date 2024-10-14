using WinterholdAPI.Books;
using WinterholdAPI.Customers;

namespace WinterholdAPI.Loans;

public class LoanDto
{
    public long Id { get; set; }
    public string BookTitle { get; set; } = null!;
    public string BookCode { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string CustomerNumber { get; set; } = null!;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
