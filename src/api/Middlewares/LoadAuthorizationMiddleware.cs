using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace api
{
    public interface ILoadUserRolesCommand
    {
        IEnumerable<string> Execute(string identity);
    }

    public class LoadUserRolesCommand : ILoadUserRolesCommand
    {
        public LoadUserRolesCommand() { }

        public IEnumerable<string> Execute(string identity)
        {
            return new string[] {
                "Teste"
            };
        }
    }
    public class LoadAuthorizationMiddleware
    {
        private RequestDelegate next;
        private ILoadUserRolesCommand loadUserRolesCommand;

        public LoadAuthorizationMiddleware(RequestDelegate next, ILoadUserRolesCommand loadUserRolesCommand)
        {
            this.next = next;
            this.loadUserRolesCommand = loadUserRolesCommand;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userClaims = this.loadUserRolesCommand.Execute(context.User.Identity.Name.ToString()).Select(role => new Claim(ClaimTypes.Role, role));
                var identity = new ClaimsIdentity(context.User.Identity, context.User.Claims.Concat(userClaims));
                context.User = new ClaimsPrincipal(identity);
            }
            return this.next(context);
        }
    }
}