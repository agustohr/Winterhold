namespace WinterholdWeb.ViewModels;

public class AuthorViewModel
{
    public long Id { get; set; }
    public string FullName { get; set; } = null!;
    public int Age { get; set; }
    public bool IsDeceased { get; set; }
    public string? Education { get; set; }
}
