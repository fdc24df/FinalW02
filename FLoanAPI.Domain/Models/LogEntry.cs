using System;

namespace FLoanAPI.Domain.Models
{
    public class LogEntry
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? Level { get; set; } 
        public string? Message { get; set; }
        public string? Exception { get; set; } 
        public USER? USER { get; set; }
        public int? USERID { get; set; } 
    }
}