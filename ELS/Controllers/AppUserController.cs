using ELS.Infrastrucure;
using ELS.Service;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ELS.Controllers
{
    public class AppUserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserStore<AppUser> _userStore;
        private IAppUserService _appUserService;
        private ITechnicianUserService _userTechnicianService;


        public AppUserController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAppUserService appUserService,
            ITechnicianUserService userTechnicianService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._appUserService = appUserService;
            this._userTechnicianService = userTechnicianService;
        }
        [HttpGet]
        public IActionResult RegisterAs() 
        {
            RegisterAsViewModel rvm = new RegisterAsViewModel();   
            return View(rvm);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAs(RegisterAsViewModel registerAsView)
        {   // same as regulate regestration
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid register data";
                return View(registerAsView);
            }

            AppUser appUser = new AppUser()
            {
                UserName = registerAsView.Email,
                FirstName = registerAsView.FirstName,
                LastName = registerAsView.LastName,
                Email = registerAsView.Email,
                FirmId = registerAsView.FirmId,
                Department = registerAsView.Department

            };

            await this._userManager.SetEmailAsync(appUser, registerAsView.Email);

            var result = await _userManager.CreateAsync(appUser, registerAsView.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError err in result.Errors)
                {

                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return View(registerAsView);
            }
            // TODO get user by email to use user id for create technician
            AppUserViewModel newUser = await _appUserService.GetUserByEmailAsync(appUser.Email);
            RegisterAsTechnicianViewModel technicianRegister = new RegisterAsTechnicianViewModel() 
            {
                FirmId = registerAsView.FirmId,
                Email = registerAsView.Email,
                FirstName = registerAsView.FirstName,
                LastName = registerAsView.LastName,
                PhoneNumber = registerAsView.PhoneNumber,
                AppUserId = newUser.Id
               
            };

            await _userTechnicianService.RegisterAsTechnician(technicianRegister);
            await _signInManager.SignInAsync(appUser, isPersistent: false);

            TempData["SuccessfullMessage"] = "You are register like technician";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel vm = new RegisterViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid register data";
                return  View(registerViewModel) ;
            }

            AppUser appUser = new AppUser()
            {
                UserName = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
                FirmId = registerViewModel.FirmId,
                Department = registerViewModel.Department

            };

           await this._userManager.SetEmailAsync(appUser, registerViewModel.Email);
            // IdentityResult result = await this._userManager.CreateAsync(appUser, registerViewModel.Password);
            var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError err in result.Errors)
                {

                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return View(registerViewModel);
            }
            TempData["SuccessMessage"] = "Successful registration";

            await _signInManager.SignInAsync(appUser, isPersistent:false);
            
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl=null) 
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            LoginViewModel model = new LoginViewModel() 
            {
                ReturnUrl = returnUrl
            
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid user information";
                return View(model);
            }
            string email =  _appUserService.UserEmail(model.FirmId);
            if (string.IsNullOrEmpty(email))
            {
                this.TempData["WarningMessage"] = "Invalid corporate identity!";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(email, model.Password, model.RemeberMe, false);
            if (!result.Succeeded)
            {
                this.TempData["ErrorMessage"] = "There was error while logging you ";
                return View(model);
            }
            this.TempData["InfoMessage"] = "Welcome back";

            return RedirectToAction("Index", "Home");


        }

    }
}
