using AuktionProjekt.Models.Entities;
using System.Collections.Generic;

namespace AuktionProjekt.Models.Repositories
{
    public interface IUserRepo
    {
        User? LoginUser(string username, string password);

        void UpdateUser(User user);

        void CreateUser(User user);

    }
}

