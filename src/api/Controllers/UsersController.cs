using System;
using domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/[controller]")]
    [Authorize(Policy = "UserManagement")]
    public class UsersController : Controller
    {
        [HttpPost]
        [Authorize(Roles = "Admin, UserCreator")]
        public IActionResult Create(User user)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new[] {
                new {
                    Id = 1,
                    Name = "Teste",
                    Email = "tiagor87@gmail.com"
                }
            });
        }
    }
}