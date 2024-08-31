using System.ComponentModel.DataAnnotations;
using Alicunde.System.Exam.Domain.Shared;

namespace Alicunde.System.Exam.Contracts.Bank;

public class BankUpdateDto
{
    [Required]
    [StringLength(BankConsts.MaxNameLength)]
    public string Name { get; set; }
    [Required]
    [StringLength(BankConsts.MaxBicLength)]
    public string Bic { get; set; }
    [Required]
    [StringLength(BankConsts.MaxCountryLength)]
    public string Country { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? DeleteAt { get; set; } = null!;
}