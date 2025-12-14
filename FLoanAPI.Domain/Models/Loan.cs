namespace FLoanAPI.Domain.Models
{
    public enum LoanType
    {
        FastLoan,
        AutoLoan,
        Installment

    }
    public enum CurrencyType
    {
        USD,
        EURO,
        GEL
    }
    public enum StatusType
    {
        inprogress,
        approved,
        rejected
    }
    public class Loan
    {
        public int ID { get; set; }
        public LoanType type { get; set; }

        public decimal Amount { get; set; }

        public CurrencyType Currency { get; set; }

        public int LoanPeriod { get; set; }

        public StatusType Status { get; set; }

        public USER? USER { get; set; }

        public int USERID { get; set; }
    }
}
