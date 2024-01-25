using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository.Repo;
using AuktionProjekt.ServiceLayer.IService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuktionProjekt.ServiceLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo repo)
        {
            _userRepo = repo;
        }


        public bool CreateUser(User user)
        {
            try
            {
                if (user.UserName is null|| user.Password is null)
                    return false;

                _userRepo.CreateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public string Login(string username, string password)
        {
            var user = _userRepo.LoginUser(username, password);

            if (user == null)
                return string.Empty;

                        
            List<Claim> claims = new List<Claim>();
            // NY KOD 2024.01.25,12:42 /Charbel
            //lägg till UserID i claim.nameindentifier -> DONE

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()));
            

            //Sätta upp kryptering. Samma säkerhetsnyckel som när vi satte upp tjänsten
            //Denna förvaras på ett säkert ställe tex Azure Keyvault eller liknande och hårdkodas
            //inte in på detta sätt
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#kjbgfoilkjgtiyduglih7gtl8gt5"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //Skapa options för att sätta upp en token
            var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5071",
                    audience: "http://localhost:5071",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials);

            //Generar en ny token som skall skickas tillbaka 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
            
        }

        public bool UpdateUser(User user)
        {
            try
            {
                if (user == null) return false;

                _userRepo.UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}

