﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using E_Saldytuvas.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IUserService _userService;

        public UsersController(ApplicationDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;

            if(_dbContext.Users.Count() == 0)
            {
                _dbContext.Users
                    .Add(new User { Name = "Vardenis", Surname = "Pavardenis" });

                _dbContext.SaveChanges();
            }
        }

        [Authorize]
        [HttpGet("claims")]
        public object Claims()
        {
            return User.Claims
                .Select(c => new
                {
                    Type = c.Type,
                    Value = c.Value
                });
        }

        [Authorize]
        [HttpPost("register")]
        public IActionResult Register()
        {
            var type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

            var claims = User.Claims
                .Select(c => new
                {
                    c.Type,
                    c.Value
                });

            var authId = claims.SingleOrDefault(c => c.Type == type)?.Value;

            if (authId == null)
                return BadRequest("Invalid token provided");

            var success = _userService.RegisterUser(authId);

            if (!success)
                return BadRequest();

            return NoContent();
        }

        // GET api/users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        // GET api/users/5
        [HttpGet("{userId}", Name = "GetUser")]
        public IActionResult GetUser(int userId)
        {
            var user = _userService.GetUser(userId);

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
            if(_userService.AddUser(user))
            {
                return CreatedAtRoute("GetUser", new { userId = user.Id }, user);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/users/5
        [HttpPut("{userId}")]
        public IActionResult UpdateUser(long userId, [FromBody] User usr)
        {
            var result = _userService.UpdateUser(userId, usr);

            if (result == -1)
            {
                return BadRequest();
            }

            if (result == -2)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        // DELETE api/users/5
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            if (_userService.DeleteUser(userId) == false)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
    }
}
