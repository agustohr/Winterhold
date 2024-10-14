namespace WinterholdWeb.ViewModels;

public class BookIndexViewModel
{
    public List<BookViewModel> Books { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
}
