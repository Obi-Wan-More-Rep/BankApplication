namespace BankApplication.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }
        public int AccountTypesId { get; set; }
        public string AccountNumber { get; set; }

        // Relationships
        public AccountType AccountType { get; set; }
        public List<Disposition> Dispositions { get; set; }
        public List<Loan> Loans { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
