namespace myfinance_web_dotnet.Domain.Models
{
    public class AccountPlan
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AccountType AccountType { get; set; } = new();
    }
}
