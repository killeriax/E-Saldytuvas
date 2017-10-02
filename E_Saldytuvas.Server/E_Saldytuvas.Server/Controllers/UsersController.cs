using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            if(_dbContext.Users.Count() == 0)
            {
                _dbContext.Users
                    .Add(new User { Name = "Vardenis", Surname = "Pavardenis" });

                _dbContext.SaveChanges();
            }
        }

        // GET api/users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            var users = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                .Include(u => u.Recipes)
                    //.ThenInclude(r => r.Ingredients)
                .ToList();

            return users;
        }

        // GET api/users/5
        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult GetUser(int userId)
        {
            var user = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                .Include(u => u.Recipes)
                .FirstOrDefault(u => u.Id == userId);

            if(user == null)
            {
                return NotFound();
            }

            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest();
            }

            _dbContext.Users
                .Add(user);

            _dbContext.SaveChanges();

            return CreatedAtRoute("GetUser", new { userId = user.Id }, user);
        }

        // PUT api/users/5
        [HttpPut("{userId}")]
        public IActionResult UpdateUser(long userId, [FromBody] User usr)
        {
            if(usr == null || usr.Id != userId)
            {
                return BadRequest();
            }

            var user = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                .Include(u => u.Recipes)
                .FirstOrDefault(u => u.Id == userId);

            if(user == null)
            {
                return NotFound();
            }

            user.Name = usr.Name;
            user.Surname = usr.Surname;

            _dbContext.Users
                .Update(user);

            _dbContext.SaveChanges();

            return new NoContentResult();
        }

        // DELETE api/users/5
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var user = _dbContext.Users
                .Include(u => u.CookedMeals)
                .Include(u => u.Ingredients)
                .Include(u => u.Recipes)
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            _dbContext.Users
                .Remove(user);

            _dbContext.SaveChanges();

            return new NoContentResult();
        }
    }
}
