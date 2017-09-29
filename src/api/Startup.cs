using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Requirements;
using domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = TokenOptions.Key,
                        ValidAudience = TokenOptions.Audience,
                        ValidIssuer = TokenOptions.Issuer,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder()
                                                            .RequireAuthenticatedUser()
                                                            .Build()));
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserManagement", policy => policy.RequireRoleWithAdmin("UserCreator", "UserViewer"));
                options.AddPolicy("AccountManagement", policy => policy.RequireRoleWithAdmin("AccountCreator", "AccountViewer"));
                options.AddPolicy("DeleteAccount", policy => policy.AddRequirements(new AccountOwnerRequirement()));
                options.AddPolicy("RequireBothRolesTest", policy => policy.RequireAllRolesWithAdmin("role1", "role2"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(policyBuilder =>
                policyBuilder.AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowAnyOrigin()
                             .AllowCredentials());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
