using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_Saldytuvas.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                .Include(u => u.Recipes)
                .ToList();

            return users;
        }

        public User GetUser(int userId)
        {
            var user = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                .Include(u => u.Recipes)
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

        public int UpdateUser(long userId, User usr)
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
    }
}
