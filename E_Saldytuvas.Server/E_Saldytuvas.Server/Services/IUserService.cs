using E_Saldytuvas.Server.Models;
using System.Collections.Generic;

namespace E_Saldytuvas.Server.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(int userId);
        bool AddUser(User user);
        int UpdateUser(long userId, User usr);
        bool DeleteUser(int userId);
        bool RegisterUser(string authId);
    }
}
