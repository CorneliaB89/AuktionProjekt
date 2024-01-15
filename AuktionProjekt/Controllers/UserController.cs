using AuktionProjekt.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuktionProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateUser(User user)
        {
            return Ok();
        }
    }
}
