using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Domain.Models;
using myfinance_web_dotnet.Infrastructure;

namespace myfinance_web_dotnet.WebApi
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountTransactionController : ControllerBase
    {
        private MyFinanceDbContext _myFinanceDbContext { get; set; }
        public AccountTransactionController(MyFinanceDbContext _myFinanceDbContext)
        {
            this._myFinanceDbContext = _myFinanceDbContext;
        }

        [HttpPost]
        public ActionResult AddTransaction(AccountTransaction accountTransaction)
        {
            try
            {
                if (accountTransaction.AccountPlan.Id == null)
                {
                    return this.Problem("AccountPlanId não pode ser nulo");
                }

                this._myFinanceDbContext.AccountTransaction.Add(new()
                {
                    Description = accountTransaction.Description,
                    DateTime = accountTransaction.DateTime,
                    Value = accountTransaction.Value,
                    AccountPlanId = (int)accountTransaction.AccountPlan.Id,
                });

                this._myFinanceDbContext.SaveChanges();

                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

        }

        [HttpGet]
        public ActionResult GetTransactions(int page = 1, int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page and pageSize must be greater than zero.");
                }

                var skip = (page - 1) * pageSize;

                var totalPlans = this._myFinanceDbContext.AccountTransaction.Count();
                var plans = this._myFinanceDbContext.AccountTransaction
                    .OrderByDescending(p => p.DateTime) // Ordene os dados para garantir consistência
                    .Skip(skip)
                    .Take(pageSize)
                    .Select(g => new AccountTransaction()
                    {
                        Id = g.Id,
                        Description = g.Description,
                        DateTime = g.DateTime,
                        Value = g.Value,
                        AccountPlan = new()
                        {
                            Id = g.AccountPlan.Id,
                            Name = g.AccountPlan.Name,
                            AccountType = new()
                            {
                                Id = (AccountTypeEnum)g.AccountPlan.Type.Id,
                                ShortName = g.AccountPlan.Type.ShortName,
                                FullName = g.AccountPlan.Type.FullName,
                                Description = g.AccountPlan.Type.Description
                            }
                        }
                    })
                    .ToList();

                return Ok(new
                {
                    Data = plans,
                    TotalCount = totalPlans,
                    Page = page,
                    PageSize = pageSize
                });
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpPut]
        public ActionResult UpdateTransactions(AccountTransaction updatedTransaction)
        {
            try{
                // Buscar o plano existente pelo ID
                var existingTransaction = this._myFinanceDbContext.AccountTransaction.FirstOrDefault(g => g.Id == updatedTransaction.Id);

                // Verificar se o plano existe
                if (existingTransaction == null)
                {
                    return this.NotFound($"Transaction with ID {updatedTransaction.Id} not found.");
                }

                if (updatedTransaction.AccountPlan.Id == null)
                {
                    return this.Problem("AccountPlanId não pode ser nulo");
                }

                // Atualizar os valores do plano existente
                existingTransaction.DateTime = updatedTransaction.DateTime;
                existingTransaction.Value = updatedTransaction.Value;
                existingTransaction.Description = updatedTransaction.Description;
                existingTransaction.AccountPlanId = (int)updatedTransaction.AccountPlan.Id;


                // Salvar as mudanças no banco de dados
                this._myFinanceDbContext.SaveChanges();

                // Retornar uma resposta de sucesso
                return this.Ok($"Transaction with ID {updatedTransaction.Id} updated successfully.");
            }
            catch (Exception ex)
            {
                // Retornar erro caso ocorra uma exceção
                return this.BadRequest($"Error updating transaction: {ex.Message}");
            }
        }

        [HttpDelete]
        public ActionResult DeleteTransaction(int id)
        {
            try
            {
                // Encontrar o plano pelo ID
                var transaction = this._myFinanceDbContext.AccountTransaction.FirstOrDefault(g => g.Id == id);

                // Verificar se o plano foi encontrado
                if (transaction == null)
                {
                    return this.NotFound($"Transaction with ID {id} not found.");
                }

                // Remover o plano do contexto
                this._myFinanceDbContext.AccountTransaction.Remove(transaction);

                // Salvar as mudanças no banco de dados
                this._myFinanceDbContext.SaveChanges();

                // Retornar uma resposta de sucesso
                return this.Ok($"Transaction with ID {id} deleted successfully."); // Ou você pode retornar Ok() se preferir
            }
            catch (Exception ex)
            {
                // Retornar erro caso ocorra uma exceção
                return this.BadRequest($"Error deleting transaction: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetMonthlySummary(int month, int year)
        {
            var groupedResults = this._myFinanceDbContext.AccountTransaction
                .Where(t => t.DateTime.Month == month && t.DateTime.Year == year) // Filtra pelo mês e ano
                .GroupBy(t => t.AccountPlan.AccountTypeId)
                .Select(g => new
                {
                    TypeId = g.Key,
                    TotalValue = g.Sum(t => t.Value)
                })
                .ToList();

            return Ok(groupedResults);
        }
    }
}
