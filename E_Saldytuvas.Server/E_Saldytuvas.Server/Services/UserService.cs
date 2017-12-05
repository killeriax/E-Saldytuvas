using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace E_Saldytuvas.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User RegisterUser(string authId)
        {
            var userIsFound = _dbContext.Users
                .SingleOrDefault(u => u.AuthId == authId);

            if (userIsFound != null)
                return null;

            var user = new User
            {
                AuthId = authId
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                //.Include(u => u.Recipes)
                .ToList();

            return users;
        }

        public User GetUser(int userId)
        {
            var user = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                //.Include(u => u.Recipes)
                .FirstOrDefault(u => u.Id == userId);

            return user;
        }

        public bool AddUser(User user)
        {
            if (user == null)
            {
                return false;
            }

            _dbContext.Users
                .Add(user);

            _dbContext.SaveChanges();

            return true;
        }

        /*public int UpdateUser(long userId, User usr)
        {
            if (usr == null || usr.Id != userId)
            {
                return -1;
            }

            var user = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                .Include(u => u.Recipes)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return -2;
            }

            user.Name = usr.Name;
            user.Surname = usr.Surname;

            _dbContext.Users
                .Update(user);

            _dbContext.SaveChanges();

            return 1;
        }*/

        public int UpdateUser(long userId, string userAuthId, User usr)
        {

            var user = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                .Include(u => u.Recipes)
                .FirstOrDefault(u => u.Id == userId);


            if (user == null)
            {
                return -2;
            }

            if (usr == null || user.AuthId != userAuthId)
            {
                return -1;
            }

            
            user.Name = usr.Name;
            user.Surname = usr.Surname;
            user.Email = usr.Email;

            _dbContext.Users
                .Update(user);

            _dbContext.SaveChanges();

            return 1;
        }

        public bool DeleteUser(int userId)
        {
            var user = _dbContext.Users
               .Include(u => u.CookedMeals)
               .Include(u => u.Ingredients)
               .Include(u => u.Recipes)
               .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            _dbContext.Users
                .Remove(user);

            _dbContext.SaveChanges();

            return true;
        }

        public string GetUserAuthId(ClaimsPrincipal claimsPrincipal)
        {
            var type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

            var claims = claimsPrincipal.Claims
                .Select(c => new
                {
                    c.Type,
                    c.Value
                });

            var authId = claims.SingleOrDefault(c => c.Type == type)?.Value;

            return authId;
        }

        public long GetUserId(string authId)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.AuthId == authId);
            if (user == null)
                return -1;
            return user.Id;
        }
    }
}
