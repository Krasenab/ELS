using AspNetCoreGeneratedDocument;
using ELS.Infrastrucure;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ELS.ViewModels.TichnicianViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELS.Controllers
{
    public class TechnicianController : Controller
    {
        private ITechnicianUserService _technicianUserService;
        private IAppUserService _appUserService;
        private ITicketService _ticketService;
        private IReportService _reportService;
        public TechnicianController(ITechnicianUserService technicianUserService, IAppUserService appUserService, 
            ITicketService ticketService, IReportService reportService)
        {
            _technicianUserService = technicianUserService;
            _appUserService = appUserService;
            _ticketService = ticketService;
            _reportService = reportService;
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
            List<ReportDetailViewModel> technicianRepots = await _reportService.GetReportsByTechnicianIdAsync(technicianId);
            int totalOpen = technicianTickets.Where(st=>st.Status=="Open").Count();
            int totalInProgress = technicianTickets.Where(st => st.Status == "InProgress").Count();
            int totalClosed = technicianTickets.Where(st => st.Status == "Closed").Count();

            TechnicianBoardViewModel viewModel = new TechnicianBoardViewModel()
            {
                TechnicianId = technicianId,
                TotalOpen = totalOpen,
                TotalInProgress = totalInProgress,
                TotalClosed = totalClosed,
                TechnicianTikets = technicianTickets,
                Reports = technicianRepots
            };


            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> TechnicianBoardBeta(string technicianId)
        {

            string asppUserId = ClaimsPrincipalExtensions.getCurrentUserId(this.User);
            bool isTechnicianExist = await _technicianUserService.IsTechnicianExistByUserIdAsync(asppUserId);
            if (!isTechnicianExist)
            {
                TempData["ErrorMessage"] = "Only technician can use this board";
                return RedirectToAction("Index", "Home");
            }

            List<TicketDetailsViewModel> technicianTickets = await _ticketService.GetTIcketsByTechnicianIdAsync(technicianId);
            List<ReportDetailViewModel> technicianRepots = await _reportService.GetReportsByTechnicianIdAsync(technicianId);
            int totalOpen = technicianTickets.Where(st => st.Status == "Open").Count();
            int totalInProgress = technicianTickets.Where(st => st.Status == "InProgress").Count();
            int totalClosed = technicianTickets.Where(st => st.Status == "Closed").Count();

            TechnicianBoardViewModel viewModel = new TechnicianBoardViewModel()
            {
                TechnicianId = technicianId,
                TotalOpen = totalOpen,
                TotalInProgress = totalInProgress,
                TotalClosed = totalClosed,
                TechnicianTikets = technicianTickets,
                Reports = technicianRepots
            };


            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> TechnicianTicketsPartial(string technicianId, int page = 1,int pageSize =5,string search="") 
        {
            PagedMyTicketsViewModel ticketsViewModel = await _ticketService.PagedAndFilteredMyTickets(technicianId, search, page, pageSize);
            return PartialView("_TechnicianTicketsPartial", ticketsViewModel);
        }

    }

}
