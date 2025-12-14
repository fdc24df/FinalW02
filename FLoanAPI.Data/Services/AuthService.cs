using FLoanAPI.Data; 
using FLoanAPI.Domain.Models;
using LoanAPI.Application;
using Microsoft.EntityFrameworkCore;

namespace FLoanAPI.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly ILoggerService _logger;

        
        public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher, ITokenService tokenService, ILoggerService logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<bool> RegisterAsync(USER user, string password)
        {
            
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                return false; 
            }

            
            user.Password = _passwordHasher.HashPassword(password);

            
            user.IsBlocked = false;
            user.Role = user.Role; 

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<(USER? User, string? Token)> LoginAsync(string username, string password)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || user.IsBlocked)
            {
                return (null, null);
            }

            
            if (!_passwordHasher.VerifyPassword(user.Password!, password))
            {
                return (null, null); 
            }

            
            var token = _tokenService.CreateToken(user);

            return (user, token);
        }
    }
}