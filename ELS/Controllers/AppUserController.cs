using Microsoft.AspNetCore.Mvc;

namespace ELS.Controllers
{
    public class AppUserController : Controller
    {
        public AppUserController()
        {
            
        }
        [HttpGet]
        public IActionResult Regiser()
        {
            return View();
        }
    }
}
