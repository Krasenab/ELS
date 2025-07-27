using ELS.Infrastrucure;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELS.Controllers
{
    public class TechnicianController : Controller
    {
        private ITechnicianUserService _technicianUserService;
        private IAppUserService _appUserService;
        public TechnicianController(ITechnicianUserService technicianUserService, IAppUserService appUserService)
        {
            _technicianUserService = technicianUserService;
            _appUserService = appUserService;
        }
        [HttpGet]
        public async Task<IActionResult> RegisterAsTechnician()
        {
            string getUserId = ClaimsPrincipalExtensions.getCurrentUserId(this.User);
            AppUserViewModel appUser = await _appUserService.GetAppUserAsync(getUserId);
            RegistierAsTechnicianViewModel model = new RegistierAsTechnicianViewModel() 
            {
                  Email = appUser.Email,
                  FirstName = appUser.FirstName,
                  LastName = appUser.LastName,
                  AppUserId = getUserId,
            };
            
            return View(model);
        }

    }
}
