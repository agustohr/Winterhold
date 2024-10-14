namespace WinterholdAPI.Loans;

public class LoanDetailDto
{
    public string Title { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Author { get; set; } = null!;
    public int Floor { get; set; }
    public string Isle { get; set; } = null!;
    public string Bay { get; set; } = null!;
    public string MembershipNumber { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? Phone { get; set; }
    public string MembershipExpiredDate { get; set; } = null!;
}
