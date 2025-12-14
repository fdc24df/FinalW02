namespace FLoanAPI.Domain.Models
{
    public enum Role
    {
        User,
        Accountant
    }
    public class USER
    {
        public int ID { get; set; }

        public string? FName { get; set; }

        public string? LName { get; set; }

        public string? Username { get; set; }
        
        public string? Email { get; set; }

        public int Age { get; set; }

        public int Salary { get; set; }

        public bool IsBlocked { get; set; }

        public string? Password { get; set; }

        public Role Role { get; set; }

        public List<Loan> Loans { get; set; } = new List<Loan>();

        public List<LogEntry> LogEntries { get; set; } = new List<LogEntry>();

    }
}
