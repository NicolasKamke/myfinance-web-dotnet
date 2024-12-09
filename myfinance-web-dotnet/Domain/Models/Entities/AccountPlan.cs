using System.ComponentModel.DataAnnotations.Schema;

namespace myfinance_web_dotnet.Domain.Models.Entities
{
    [Table("account_plan")] // Mapeia para a tabela "account_plan" no banco de dados
    public class AccountPlan
    {
        // Propriedades
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("account_type_id")]
        public int AccountTypeId { get; set; }

        // Relacionamentos
        public virtual AccountType Type { get; set; } = null!; // Relacionamento com Type
        public virtual ICollection<AccountTransaction> Transactions { get; set; } = new List<AccountTransaction>();
    }
}
