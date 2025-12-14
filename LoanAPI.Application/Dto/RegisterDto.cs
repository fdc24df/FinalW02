using FLoanAPI.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace LoanAPI.Application.Dto
{
    public class RegisterDto
    {
        [Required]
        public string? FName { get; set; }

        [Required]
        public string? LName { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        // 🔴 Role აუცილებელია, მაგრამ შესაძლოა არ იყოს Required, თუ ყოველთვის User-ია
        public Role Role { get; set; } = Role.User; // ნაგულისხმევი მნიშვნელობა
    }
}
