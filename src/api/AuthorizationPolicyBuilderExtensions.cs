using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace api
{
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireRoleWithAdmin(this AuthorizationPolicyBuilder builder, params string[] roles)
        {
            return builder.RequireRole(roles.Concat(new string[] { "Admin" }));
        }

        public static AuthorizationPolicyBuilder RequireAllRolesWithAdmin(this AuthorizationPolicyBuilder builder, params string[] roles)
        {
            foreach (var role in roles)
            {
                builder.RequireRoleWithAdmin(role);
            }
            return builder;
        }
    }
}