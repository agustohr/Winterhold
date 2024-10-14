using System.ComponentModel.DataAnnotations;

namespace WinterholdWeb.ViewModels;

public class AuthorUpsertViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Title Must Be Filled!")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "First Name Must Be Filled!")]
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Birth Date Must Be Filled!")]
    public DateTime? BirthDate { get; set; }
    public DateTime? DeceasedDate { get; set; }
    public string? Education { get; set; }
    public string? Summary { get; set; }
}
