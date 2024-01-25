using AuktionProjekt.Models.Entities;
using AuktionProjekt.Repository.Repo;
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
        // NY KOD 2024.01.25,04:01 /Charbel
        // Skapar constructor för att tillåta UserController använda UserRepo
        private readonly UserRepo _userRepo;

        public UserController(UserRepo userRepo)
        {
            _userRepo = userRepo;
        } //SLUT

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            // NY KOD 2024.01.25,12:51 /Charbel
            _userRepo.CreateUser(user);
            return Ok(); //SLUT
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // NY KOD 2024.01.25,04:11 /Charbel
            // Kör repometoden för att kolla inloggningen -> Done
            // om den är null retunera badrequest("Invalied login") -> Done
            var user = _userRepo.LoginUser(username, password);

            if (user == null)
            {
                return BadRequest("Felaktig inloggning, försök igen!");
            } // SLUT




            //gör en haskoll på Lösenordet

            List<Claim> claims = new List<Claim>();
            // NY KOD 2024.01.25,12:42 /Charbel
            //lägg till UserID i claim.nameindentifier -> DONE
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString());
            };

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
            _userRepo.UpdateUser(user);
            return Ok();
        }
    }
}
