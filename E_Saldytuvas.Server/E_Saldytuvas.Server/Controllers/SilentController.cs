using Microsoft.AspNetCore.Mvc;

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
