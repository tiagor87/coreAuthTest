using System.Threading.Tasks;
using api.Requirements;
using domain.Models;
using domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/[controller]")]
    [Authorize(Policy = "AccountManagement")]
    public class AccountController : Controller
    {
        private IAuthorizationService authorizationService;
        public AccountController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, AccountCreator")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            using (var service = new AccountService())
            {
                Account account = service.GetById(id);
                var result = await this.authorizationService.AuthorizeAsync(User, account, "DeleteAccount");
                if (result.Succeeded)
                {
                    return Ok();
                }
                return Forbid();
            }
        }

        [HttpGet]
        [Authorize(Policy = "RequireBothRolesTest")]
        public IActionResult Teste()
        {
            return Ok();
        }
    }
}