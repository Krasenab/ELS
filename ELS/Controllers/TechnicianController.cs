using AspNetCoreGeneratedDocument;
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
        private ITicketService _ticketService;
        public TechnicianController(ITechnicianUserService technicianUserService, IAppUserService appUserService, ITicketService ticketService)
        {
            _technicianUserService = technicianUserService;
            _appUserService = appUserService;
            _ticketService = ticketService;
        }
        [HttpGet]
        public async Task<IActionResult> RegisterAsTechnician()
        {
            string getUserId = ClaimsPrincipalExtensions.getCurrentUserId(this.User);
            AppUserViewModel appUser = await _appUserService.GetAppUserAsync(getUserId);
            bool isUserTechincian = await _technicianUserService.IsTechnicianExistByUserIdAsync(getUserId);

            if (isUserTechincian==true)
            {
                TempData["WarningMessage"] = "You are already a technician, you cannot register as a technician again.";
                return RedirectToAction("Index", "Home");
            }

            RegisterAsTechnicianViewModel model = new RegisterAsTechnicianViewModel() 
            {
                  Email = appUser.Email,
                  FirstName = appUser.FirstName,
                  LastName = appUser.LastName,
                  
            };
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsTechnician(RegisterAsTechnicianViewModel regiserViewMode) 
        {
            string getUserId = ClaimsPrincipalExtensions.getCurrentUserId(this.User);
            regiserViewMode.AppUserId = getUserId;
            await _technicianUserService.RegisterAsTechnician(regiserViewMode);
            TempData["SuccessMessage"] = "You have successfully joined as a technician.";
            return  RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> TechnicianBoard(string technicianId) 
        {
         
            string asppUserId = ClaimsPrincipalExtensions.getCurrentUserId(this.User);
            bool isTechnicianExist = await _technicianUserService.IsTechnicianExistByUserIdAsync(asppUserId);
            if (!isTechnicianExist)
            {
                TempData["ErrorMessage"] = "Only technician can use this board";
                return RedirectToAction("Index", "Home");
            }

            List<TicketDetailsViewModel> technicianTickets = await _ticketService.GetTIcketsByTechnicianIdAsync(technicianId);

            int totalOpen = technicianTickets.Where(st=>st.Status=="Open").Count();
            int totalInProgress = technicianTickets.Where(st => st.Status == "InProgress").Count();
            int totalClosed = technicianTickets.Where(st => st.Status == "Closed").Count();

            TechnicianBoardViewModel viewModel = new TechnicianBoardViewModel()
            {
                TechnicianId = technicianId,
                TotalOpen = totalOpen,
                TotalInProgress = totalInProgress,
                TotalClosed = totalClosed,
                TechnicianTikets = technicianTickets
            };


            return View(viewModel);
        }
        

    }
}
