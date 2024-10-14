namespace WinterholdWeb.ViewModels;

public class CategoryViewModel
{
    public string Name { get; set; } = null!;
    public int Floor { get; set; }
    public string Isle { get; set; } = null!;
    public string Bay { get; set; } = null!;
    public int TotalBooks { get; set; }
}
