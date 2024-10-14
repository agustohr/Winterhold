namespace WinterholdWeb.ViewModels;

public class LoanViewModel
{
    public long Id { get; set; }
    public string BookTitle { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string LoanDate { get; set; } = null!;
    public string DueDate { get; set; } = null!;
    public string? ReturnDate { get; set; }
}
