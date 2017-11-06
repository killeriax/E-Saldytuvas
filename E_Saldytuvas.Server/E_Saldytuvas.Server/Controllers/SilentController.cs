using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using E_Saldytuvas.Server.Data;
using E_Saldytuvas.Server.Models;
using E_Saldytuvas.Server.Services;

namespace E_Saldytuvas.Server.Controllers
{
    [Route("api/[controller]")]
    public class SilentController : Controller
    {
        public SilentController()
        {
        }

        // GET: api/silent
        [HttpGet]
        public IActionResult Silent()
        {
            ViewData["FrontEndUrl"] = "http://localhost:3000";

            return View();
        }

    }
}
