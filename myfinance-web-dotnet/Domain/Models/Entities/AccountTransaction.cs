using System.ComponentModel.DataAnnotations.Schema;

namespace myfinance_web_dotnet.Domain.Models.Entities
{
    [Table("account_transaction")] // Mapeia para a tabela "transactions" no banco de dados
    public class AccountTransaction
    {
        // Propriedades
        [Column("id")]
        public int Id { get; set; }

        [Column("date_time")]
        public DateTime DateTime { get; set; }

        [Column("value")]
        public decimal Value { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("account_plan_id")]
        public int AccountPlanId { get; set; }

        // Relacionamentos
        public virtual AccountPlan AccountPlan { get; set; } = null!;
    }
}
