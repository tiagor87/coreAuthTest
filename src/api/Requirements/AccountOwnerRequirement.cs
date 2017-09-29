using System.Threading.Tasks;
using domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace api.Requirements
{
    public class AccountOwnerRequirement : AuthorizationHandler<AccountOwnerRequirement, Account>, IAuthorizationRequirement
    {
        public AccountOwnerRequirement() : base()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccountOwnerRequirement requirement, Account account)
        {
            if (context.User == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (context.User.IsInRole("Admin") || account.Owner.Equals(context.User.Identity.Name))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}