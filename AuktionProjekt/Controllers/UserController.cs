using AuktionProjekt.Models.Entities;
using AuktionProjekt.Repository.Repo;
using AuktionProjekt.ServiceLayer.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuktionProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("Create/User")]
        [HttpPost]
        public IActionResult CreateUser(User user)
        {

            bool check = _userService.CreateUser(user);
            if (check)
                return Ok("Kund skapad");

            return StatusCode(400, "Error, användare kunde ej skapas.");
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                string tokenString = _userService.Login(username, password);

                if (string.IsNullOrEmpty(tokenString))
                    return BadRequest("Felaktig inloggning");

                return Ok(new { Token = tokenString });
            }
            catch (Exception)
            {
                return StatusCode(500,"ERROR, login");
                throw;
            }

        }
        [Route("Updaate/User")]
        [HttpPut]
        [Authorize]
        public IActionResult UpdateUser(User user)
        {
            var inlogedUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            user.UserID = int.Parse(inlogedUser);

            bool check = _userService.UpdateUser(user);

            if (check)
                return Ok("Ditt konto är uppdterat");

            return StatusCode(400, "Kunde inte uppdatera");
        }
    }
}
