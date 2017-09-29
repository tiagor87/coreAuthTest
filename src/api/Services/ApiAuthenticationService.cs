using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class ApiAuthenticationService
    {
        public string SignIn(string login, string password)
        {
            User user;
            if (login.Equals("Admin"))
            {
                user = new User
                {
                    Id = 1,
                    Login = "Admin",
                    Email = "admin@admin.com",
                    Roles = { "Admin" }
                };
            }
            else if (login.Equals("User"))
            {
                user = new User
                {
                    Id = 2,
                    Login = "User",
                    Email = "user@user.com",
                    Roles = { "User", "UserViewer", "AccountCreator", "role1", "role2" }
                };
            }
            else
            {
                throw new Exception("Did you forget your password? Try to reset it.");
            }

            var handler = new JwtSecurityTokenHandler();

            var identity = this.CreateIdentity(user);

            var now = DateTime.Now;

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenOptions.Issuer,
                Audience = TokenOptions.Audience,
                SigningCredentials = TokenOptions.SigningCredentials,
                Subject = identity,
                Expires = now + TokenOptions.ExpiresSpan,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now
            });
            return handler.WriteToken(securityToken);
        }

        private ClaimsIdentity CreateIdentity(User user)
        {
            return new ClaimsIdentity(new GenericIdentity(user.ToString(), ClaimTypes.NameIdentifier),
                new Claim[] {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Email, user.Email)
                }.Concat(user.Roles.ConvertAll(r => new Claim(ClaimTypes.Role, r)))
            );
        }
    }
}