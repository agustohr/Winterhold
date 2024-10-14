using System.ComponentModel.DataAnnotations;
using WinterholdWeb.Validations;

namespace WinterholdWeb.ViewModels;

public class CustomerUpsertViewModel
{
    [Required(ErrorMessage = "Membership Number Must Be Filled!")]
    [UniqueMembershipNumberValidation]
    public string MembershipNumber { get; set; } = null!;

    [Required(ErrorMessage = "First Name Must Be Filled!")]
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Birth Date Must Be Filled!")]
    public DateTime? BirthDate { get; set; }

    [Required(ErrorMessage = "Gender Must Be Filled!")]
    public string Gender { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
}
