using AuktionProjekt.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuktionProjekt.ServiceLayer.IService
{
    public interface IUserService
    {
        void CreateUser(User user);
        string Login(string username, string password);
        void UpdateUser(User user);
    }
}
