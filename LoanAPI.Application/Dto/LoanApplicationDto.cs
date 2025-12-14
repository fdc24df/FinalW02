using System.ComponentModel.DataAnnotations;

namespace LoanAPI.Application.DTOs
{
    public class LoanApplicationDto
    {
        [Required]
        public string? Type { get; set; } 

        [Required, Range(100, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        public string? Currency { get; set; } 

        [Required, Range(1, 60)]
        public int LoanPeriod { get; set; } 
    }
}