using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace api.Requirements
{
    public class PermissionRequirement : AuthorizationHandler<PermissionRequirement>, IAuthorizationRequirement
    {
        private string[] roles;
        public PermissionRequirement(params string[] roles)
        {
            this.roles = roles;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (context.User.IsInRole("Admin") || this.IsGranted(context.User))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }

        private bool IsGranted(ClaimsPrincipal identity)
        {
            return this.roles.Any(r => identity.HasClaim(ClaimTypes.Role, r));
        }
    }
}