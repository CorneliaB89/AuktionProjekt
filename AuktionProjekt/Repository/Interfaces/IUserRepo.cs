using AuktionProjekt.Models.Entities;
using System.Collections.Generic;

namespace AuktionProjekt.Models.Repositories
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        void UpdateUser(User user);
        // lägg till flera metoder för att synka med databsen.
    }
}

