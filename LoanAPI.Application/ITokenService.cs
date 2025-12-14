using FLoanAPI.Domain.Models;

namespace LoanAPI.Application
{
    public interface ITokenService
    {
        string CreateToken(USER user);
    }
}
