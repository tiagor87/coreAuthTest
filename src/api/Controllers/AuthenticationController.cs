using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Contracts;
using api.Services;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{

    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private ApiAuthenticationService authenticationService;

        public AuthenticationController()
        {
            this.authenticationService = new ApiAuthenticationService();
        }

        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] AuthenticationContract auth)
        {
            var token = this.authenticationService.SignIn(auth.Login, auth.Password);
            return Ok(new
            {
                Token = token
            });
        }


    }
}
