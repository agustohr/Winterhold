using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WinterholdWeb.Validations;

namespace WinterholdWeb.ViewModels;

public class BookUpsertViewModel
{
    [Required(ErrorMessage = "Book Code Must Be Filled!")]
    [UniqueBookCodeValidation]
    public string BookCode { get; set; } = null!;

    [Required(ErrorMessage = "Title Must Be Filled!")]
    public string Title { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Author Must Be Filled!")]
    public long AuthorId { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? TotalPage { get; set; }
    public string? Summary { get; set; }
    public List<SelectListItem>? Authors { get; set; }
}
