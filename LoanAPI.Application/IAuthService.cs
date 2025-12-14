using FLoanAPI.Domain.Models; 

namespace LoanAPI.Application
{
    
    public interface IAuthService
    {
        
        Task<bool> RegisterAsync(USER user, string password);

        
        Task<(USER? User, string? Token)> LoginAsync(string username, string password);
    }
}

