namespace WinterholdAPI.Loans;

public class LoanUpsertDto
{
    public long Id { get; set; }
    public string BookCode { get; set; } = null!;
    public string CustomerNumber { get; set; } = null!;
    public DateTime LoanDate { get; set; }
    public string? Note { get; set; }
}
