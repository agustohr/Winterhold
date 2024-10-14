namespace WinterholdWeb.ViewModels;

public class AuthorIndexViewModel
{
    public List<AuthorViewModel> Authors { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string AuthorName { get; set; }
}
