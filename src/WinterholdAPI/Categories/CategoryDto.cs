namespace WinterholdAPI.Categories;

public class CategoryDto
{
    public string Name { get; set; } = null!;
    public int Floor { get; set; }
    public string Isle { get; set; } = null!;
    public string Bay { get; set; } = null!;
    public int TotalBooks { get; set; }
}
