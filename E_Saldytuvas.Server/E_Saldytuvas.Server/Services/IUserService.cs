using E_Saldytuvas.Server.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace E_Saldytuvas.Server.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(int userId);
        bool AddUser(User user);
        //int UpdateUser(long userId, User usr);
        int UpdateUser(long userId, string userAuthId, User usr);
        bool DeleteUser(int userId);
        User RegisterUser(string authId);
        string GetUserAuthId(ClaimsPrincipal claimsPrincipal);
        long GetUserId(string authId);
    }
}
