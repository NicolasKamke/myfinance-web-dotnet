using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Domain.Models;
using myfinance_web_dotnet.Infrastructure;
using System.Text.Json;

namespace myfinance_web_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountTypeController : ControllerBase
    {
        private MyFinanceDbContext _myFinanceDbContext { get; set; }
        public AccountTypeController(MyFinanceDbContext _myFinanceDbContext)
        {
            this._myFinanceDbContext = _myFinanceDbContext;
        }

        [HttpGet]
        public ActionResult<List<AccountType>> GetTypes()
        {
            try
            {
                var types = this._myFinanceDbContext.AccountType
                    .Select(g => new AccountType
                    {
                        Id = (AccountTypeEnum)g.Id,
                        ShortName = g.ShortName,
                        FullName= g.FullName,
                        Description = g.Description,
                    })
                    .ToList();

                if (types?.Count != 0)
                {
                    return types;
                }

                return this.NoContent();
                
            }
            catch
            {
                return this.BadRequest();
            }
        }
    }
}
