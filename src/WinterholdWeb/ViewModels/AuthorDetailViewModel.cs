namespace WinterholdWeb.ViewModels;

public class AuthorDetailViewModel
{
    public long Id { get; set; }
    public string FullName { get; set; } = null!;
    public string BirthDate { get; set; } = null!;
    public string? DeceasedDate { get; set; }
    public string? Education { get; set; }
    public string? Summary { get; set; }

    public List<BookViewModel> Books { get; set; } = null!;
    public PaginationViewModel PaginationBooks { get; set; }
}
