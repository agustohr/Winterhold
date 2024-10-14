namespace WinterholdWeb.ViewModels;

public class LoanIndexViewModal
{
    public List<LoanViewModel> Loans { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string BookTitle { get; set; }
    public string CustomerName { get; set; }
    public bool IsPassed { get; set; }
}
