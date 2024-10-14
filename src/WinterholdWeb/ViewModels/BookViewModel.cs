namespace WinterholdWeb.ViewModels;

public class BookViewModel
{
    public string Code { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string AuthorName { get; set; } = null!;
    public string LoanStatus { get; set; } = null!;
    public string? Summary { get; set; }
    public string? ReleaseDate { get; set; }
    public int? TotalPage { get; set; }
}
