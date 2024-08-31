using System.ComponentModel.DataAnnotations;
using Alicunde.System.Exam.Domain.Shared;

namespace Alicunde.System.Exam.Domain
{
    public class Bank : BaseTrackableEntity
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
    }
}