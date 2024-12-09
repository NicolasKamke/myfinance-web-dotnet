namespace myfinance_web_dotnet.Domain.Models
{
    public class AccountType
    {
        public AccountTypeEnum Id { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
