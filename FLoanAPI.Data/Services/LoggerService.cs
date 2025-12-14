using FLoanAPI.Domain.Models; 
using LoanAPI.Application;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FLoanAPI.Data.Services
{

    public class LoggerService : ILoggerService
    {
        private readonly ApplicationDbContext _context;

        public LoggerService(ApplicationDbContext context)
        {
            _context = context;
        }
        private async Task SaveLog(string level, string message, Exception? ex = null, int? userId = null)
        {
            var logEntry = new LogEntry
            {
                
                Timestamp = DateTime.UtcNow,
                Level = level,
                Message = message,
                Exception = ex?.ToString(), 
                USERID = userId
            };

            _context.Logs.Add(logEntry);
            await _context.SaveChangesAsync();
        }

        
        public Task LogErrorAsync(string message, Exception? ex, int? userId = null)
        {
            return SaveLog("Error", message, ex, userId);
        }

        public Task LogInfoAsync(string message, int? userId = null)
        {
            return SaveLog("Info", message, null, userId); 
        }

        public Task LogWarningAsync(string message, int? userId = null)
        {
            return SaveLog("Warning", message, null, userId); 
        }
    }
}
