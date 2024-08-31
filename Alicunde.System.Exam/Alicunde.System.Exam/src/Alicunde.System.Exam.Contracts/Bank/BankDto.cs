namespace Alicunde.System.Exam.Contracts.Bank;

public class BankDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Bic { get; set; }
    public string Country { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeleteAt { get; set; }
}

