using AuktionProjekt.Models.Entities;
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

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            return Ok();
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Kör repometoden för att kolla inloggningen

            // om den är null retunera badrequest("Invalied login")

            //gör en haskoll på Lösenordet

            List<Claim> claims = new List<Claim>();
            //lägg till UserID i claim.nameindentifier

            //Sätta upp kryptering. Samma säkerhetsnyckel som när vi satte upp tjänsten
            //Denna förvaras på ett säkert ställe tex Azure Keyvault eller liknande och hårdkodas
            //inte in på detta sätt
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#kjbgfoilkjgtiyduglih7gtl8gt5"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //Skapa options för att sätta upp en token
            var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5042",
                    audience: "http://localhost:5042",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials);

            //Generar en ny token som skall skickas tillbaka 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });

        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdateUser(User user)
        {
            return Ok();
        }
    }
}
