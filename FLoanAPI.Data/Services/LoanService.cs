using FLoanAPI.Data;
using FLoanAPI.Domain.Models;
using LoanAPI.Application;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FLoanAPI.Data.Services
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggerService _logger;

        public LoanService(ApplicationDbContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Loan?> ApplyForLoanAsync(Loan loan)
        {
           
            loan.Status = StatusType.inprogress; 
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            await _logger.LogInfoAsync($"New loan application created for UserID: {loan.USERID}", loan.USERID);
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetUserLoansAsync(int userId)
        {
            
            return await _context.Loans
                .Where(l => l.USERID == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetAllLoanApplicationsAsync()
        {
            
            return await _context.Loans.ToListAsync();
        }

        public async Task<Loan?> UpdateLoanStatusAsync(int loanId, StatusType newStatus)
        {
            var loan = await _context.Loans.FindAsync(loanId);

            if (loan == null)
            {
                await _logger.LogWarningAsync($"Loan update failed: LoanID {loanId} not found.", null);
                return null;
            }

            loan.Status = newStatus;
            await _context.SaveChangesAsync();

            await _logger.LogInfoAsync($"Loan status updated to {newStatus} for LoanID: {loanId}", loan.USERID);
            return loan;
        }
    }
}
