namespace myfinance_web_dotnet.Domain.Models
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
        public string? Description { get; set; }
        public AccountPlan AccountPlan { get; set; } = new();
    }
}
