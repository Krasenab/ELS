using ELS.Service.Interfaces;
using ELS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELS.Controllers
{
    public class TechnicianController : Controller
    {
        private ITechnicianUserService _technicianUserService;
        public TechnicianController(ITechnicianUserService technicianUserService)
        {
            _technicianUserService = technicianUserService;
        }
        [HttpGet]
        public IActionResult RegisterAsTechnician()
        {
            RegistierAsTechnicianViewModel model = new RegistierAsTechnicianViewModel();
            return View();
        }
    }
}
