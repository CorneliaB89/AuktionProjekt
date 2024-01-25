using AuktionProjekt.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuktionProjekt.ServiceLayer.IService
{
    public interface IUserService
    {
        bool CreateUser(User user);
        string Login(string username, string password);
        bool UpdateUser(User user);
    }
}
