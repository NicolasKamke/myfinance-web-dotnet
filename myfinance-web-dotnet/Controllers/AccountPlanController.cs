using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Domain.Models;
using myfinance_web_dotnet.Infrastructure;

namespace myfinance_web_dotnet.WebApi
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountPlanController : ControllerBase
    {
        private MyFinanceDbContext _myFinanceDbContext { get; set; }
        public AccountPlanController(MyFinanceDbContext _myFinanceDbContext)
        {
            this._myFinanceDbContext = _myFinanceDbContext;
        }

        [HttpPost]
        public ActionResult AddPlan(AccountPlan accountPlan)
        {
            try
            {
                if(this._myFinanceDbContext.AccountPlan.Any(x => x.Name == accountPlan.Name))
                {
                    return this.Conflict("Name already registered");
                }

                this._myFinanceDbContext.AccountPlan.Add(new()
                {
                    AccountTypeId = (int)accountPlan.AccountType.Id,
                    Name = accountPlan.Name,
                });

                this._myFinanceDbContext.SaveChanges();

                return this.Ok();
            }
            catch(Exception ex)
            {
                return this.BadRequest(ex);
            }
            
        }

        [HttpGet]
        public ActionResult<List<AccountPlan>> GetPlans(int page = 1, int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page and pageSize must be greater than zero.");
                }

                var skip = (page - 1) * pageSize;

                var totalPlans = this._myFinanceDbContext.AccountPlan.Count();
                var plans = this._myFinanceDbContext.AccountPlan
                    .OrderBy(p => p.Id) // Ordene os dados para garantir consistência
                    .Skip(skip)
                    .Take(pageSize)
                    .Select(g => new AccountPlan()
                    {
                        Name = g.Name,
                        AccountType = new()
                        {
                            Id = (AccountTypeEnum)g.Type.Id,
                            Description = g.Type.Description,
                            FullName = g.Type.FullName,
                            ShortName = g.Type.ShortName
                        },                      
                        Id = g.Id
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

        [HttpGet]
        public ActionResult<List<AccountPlan>> GetAllPlans()
        {
            try
            {
                return this._myFinanceDbContext.AccountPlan
                    .OrderBy(p => p.Name) // Ordene os dados para garantir consistência
                    .Select(g => new AccountPlan()
                    {
                        Name = g.Name,
                        AccountType = new()
                        {
                            Id = (AccountTypeEnum)g.Type.Id,
                            Description = g.Type.Description,
                            FullName = g.Type.FullName,
                            ShortName = g.Type.ShortName
                        },
                        Id = g.Id
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpDelete]
        public ActionResult DeletePlan(int id)
        {
            try
            {
                // Encontrar o plano pelo ID
                var plan = this._myFinanceDbContext.AccountPlan.FirstOrDefault(g => g.Id == id);

                // Verificar se o plano foi encontrado
                if (plan == null)
                {
                    return this.NotFound($"Plan with ID {id} not found.");
                }

                // Remover o plano do contexto
                this._myFinanceDbContext.AccountPlan.Remove(plan);

                // Salvar as mudanças no banco de dados
                this._myFinanceDbContext.SaveChanges();

                // Retornar uma resposta de sucesso
                return this.Ok($"Plan with ID {id} deleted successfully."); // Ou você pode retornar Ok() se preferir
            }
            catch (Exception ex)
            {
                // Retornar erro caso ocorra uma exceção
                return this.BadRequest($"Error deleting plan: {ex.Message}");
            }
        }

        [HttpPut]
        public ActionResult UpdatePlan(AccountPlan updatedPlan)
        {
            try
            {
                // Buscar o plano existente pelo ID
                var existingPlan = this._myFinanceDbContext.AccountPlan.FirstOrDefault(g => g.Id == updatedPlan.Id);

                // Verificar se o plano existe
                if (existingPlan == null)
                {
                    return this.NotFound($"Plan with ID {updatedPlan.Id} not found.");
                }

                // Atualizar os valores do plano existente
                existingPlan.Name = updatedPlan.Name;
                existingPlan.AccountTypeId = (int)updatedPlan.AccountType.Id;

                // Salvar as mudanças no banco de dados
                this._myFinanceDbContext.SaveChanges();

                // Retornar uma resposta de sucesso
                return this.Ok($"Plan with ID {updatedPlan.Id} updated successfully.");
            }
            catch (Exception ex)
            {
                // Retornar erro caso ocorra uma exceção
                return this.BadRequest($"Error updating plan: {ex.Message}");
            }
        }
    }
}
