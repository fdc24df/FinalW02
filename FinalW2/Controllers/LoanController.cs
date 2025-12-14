using FLoanAPI.Domain.Models;
using LoanAPI.Application;
using LoanAPI.Application.DTOs;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalW2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly ILoggerService _logger;

        public LoanController(ILoanService loanService, ILoggerService logger)
        {
            _loanService = loanService;
            _logger = logger;
        }

        private int GetUserIdFromToken()
        {
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                
                throw new UnauthorizedAccessException("User ID claim not found in token.");
            }
            return userId;
        }

        
        [HttpPost("apply")]
        [Authorize(Roles = "User, Admin")] 
        public async Task<IActionResult> Apply([FromBody] LoanApplicationDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var userId = GetUserIdFromToken();

                var loan = new Loan
                {
                    Amount = dto.Amount,
                    Currency = Enum.Parse<CurrencyType>(dto.Currency!, true),
                    LoanPeriod = dto.LoanPeriod,
                    type = Enum.Parse<LoanType>(dto.Type!, true),
                    USERID = userId,
                };

                var newLoan = await _loanService.ApplyForLoanAsync(loan);

                return StatusCode(201, newLoan);
            }
            catch (UnauthorizedAccessException ex)
            {
                await _logger.LogWarningAsync("Token error during loan application.", null);
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpGet("my-loans")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetUserLoans()
        {
            try
            {
                var userId = GetUserIdFromToken();
                var loans = await _loanService.GetUserLoansAsync(userId);

                return Ok(loans);
            }
            catch (UnauthorizedAccessException ex)
            {
                await _logger.LogWarningAsync("Token error during getting user loans.", null);
                return Unauthorized(new { Message = ex.Message });
            }
        }

        
        [HttpGet("all")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllLoanApplicationsAsync();
            return Ok(loans);
        }

        
        [HttpPut("status/{loanId}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> UpdateStatus(int loanId, [FromQuery] StatusType newStatus)
        {
            var updatedLoan = await _loanService.UpdateLoanStatusAsync(loanId, newStatus);

            if (updatedLoan == null)
            {
                return NotFound(new { Message = $"Loan with ID {loanId} not found." });
            }

            return Ok(updatedLoan);
        }
    }
}