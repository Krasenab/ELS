using ELS.Service;
using ELS.Service.Interfaces;
using ElsModels.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELS.Controllers
{
    public class AppUserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private IAppUserService _appUserService;


        public AppUserController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAppUserService appUserService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._appUserService = appUserService;

        }

        [HttpGet]
        public IActionResult Regiser()
        {

            return View();
        }
    }
}
