using System;
using System.Threading.Tasks;

namespace LoanAPI.Application
{
    public interface ILoggerService
    {
        
        Task LogErrorAsync(string message, Exception ex, int? userId = null);

        Task LogInfoAsync(string message, int? userId = null);

        Task LogWarningAsync(string message, int? userId = null);
    }
}
