using System.ComponentModel.DataAnnotations.Schema;

namespace myfinance_web_dotnet.Domain.Models.Entities
{
    [Table("account_type")] // Mapeia para a tabela "account_plan" no banco de dados
    public class AccountType
    {
        // Propriedades
        [Column("id")]
        public int Id { get; set; }

        [Column("short_name")]
        public string ShortName { get; set; } = string.Empty; // Renomeado para evitar conflito com a palavra reservada "type"

        [Column("full_name")]
        public string FullName { get; set; } = string.Empty; // Renomeado para evitar conflito com a palavra reservada "type"

        [Column("description")]
        public string? Description { get; set; }

        // Relacionamentos
        public virtual ICollection<AccountPlan> AccountPlans { get; set; } = new List<AccountPlan>();
    }
}
