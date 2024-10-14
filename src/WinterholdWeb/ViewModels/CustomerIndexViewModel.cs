namespace WinterholdWeb.ViewModels;

public class CustomerIndexViewModel
{
    public List<CustomerViewModel> Customers { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string MemberNumber { get; set; }
    public string FullName { get; set; }
    public bool IsExpired { get; set; }
}
