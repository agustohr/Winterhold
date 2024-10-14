namespace WinterholdWeb.ViewModels;

public class CategoryIndexViewModel
{
    public List<CategoryViewModel> Categories { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string CategoryName { get; set; }
}
