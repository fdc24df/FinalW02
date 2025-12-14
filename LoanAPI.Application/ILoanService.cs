using FLoanAPI.Domain.Models;
using System.Collections.Generic;

namespace LoanAPI.Application
{
    public interface ILoanService
    {
        Task<Loan?> ApplyForLoanAsync(Loan loan);

        Task<IEnumerable<Loan>> GetUserLoansAsync(int userId);

        Task<IEnumerable<Loan>> GetAllLoanApplicationsAsync();

        Task<Loan?> UpdateLoanStatusAsync(int loanId, StatusType newStatus);
    }
}