using AuktionProjekt.Models.Entities;
using AuktionProjekt.Models.Repositories;
using AuktionProjekt.Repository.Repo;
using AuktionProjekt.ServiceLayer.IService;

namespace AuktionProjekt.ServiceLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo repo)
        {
            _userRepo = repo;
        }


        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public string Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
}
