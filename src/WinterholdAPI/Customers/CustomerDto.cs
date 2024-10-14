namespace WinterholdAPI.Customers;

public class CustomerDto
{
    public string MembershipNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string BirthDate { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
}
