using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AuktionProjekt.Repository.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly IAucktionDBContext _dbContext;
        public UserRepo(IAucktionDBContext aucktionDBContext)
        {
            _dbContext = aucktionDBContext;
        }

        // LoginUser Method
        public User? LoginUser(string username, string password)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                parameters.Add("@Password", password);

                return db.Query<User>("Login", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }



        // Update Method

        public void UpdateUser(User user)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserID", user.UserID);
                parameters.Add("@Username", user.UserName);
                parameters.Add("@Password", user.Password);

                db.Execute("UpdateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }




        // Create User method
        public void CreateUser(User user)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Username", user.UserName, DbType.String);
                parameters.Add("@Password", user.Password, DbType.String);

                db.Execute("CreateUser", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}




