using ELS.Infrastrucure;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ELS.ViewModels.RportsVIewModels;
using ElsModels.SQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace ELS.Controllers
{
    public class ReportController : Controller
    {
        private IReportService _reportService;
        private ITechnicianUserService _tecUserService;
        public ReportController(IReportService reportService, ITechnicianUserService tecUserService)
        {
            this._reportService = reportService;
            _tecUserService = tecUserService;
        }
        [HttpGet]
        public async Task<IActionResult> CreateReport(string ticketId)
        {
            string userId = ClaimsPrincipalExtensions.getCurrentUserId(this.User);
            string technicianUserId = await _tecUserService.GetTechnicianIdAsync(userId);
            // to do check for exist report for the ticket 
            bool hasReport = await _reportService.HasReportForTicketAsync(ticketId);
            if (hasReport==true)
            {
                TempData["WarningMessage"] = "Ticket has report.Ticket is closed";
                return RedirectToAction("TechnicianBoard", "Technician",  new { technicianId = technicianUserId });
            }
            ReportCreateViewModel model = new ReportCreateViewModel()
            {
                TicketId = ticketId,
                TechnicianId = technicianUserId
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateReport(ReportCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Model is not valid";
                return View();
            }
           
            TempData["SucssesfullMessage"] = "Repot has been send successfully";
            _reportService.CreateReport(viewModel);
            return RedirectToAction("Index", "Home");   
        }

        [HttpGet]
        public async Task<IActionResult> ReportDetails(string reportId) 
        {
            ReportDetailViewModel report = await _reportService.GetReportAsync(reportId);
            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> AllReports() 
        {
            AllReportMainViewModel reports = new AllReportMainViewModel()
            {
                SearchTerm = "",
                Reports = await _reportService.GetAllReportsAsync()
            };

            return View(reports);
        }

        [HttpGet]
        public async Task<IActionResult> AllFilteredReports(string searchTerm,int page=1) 
        {
           AllReportMainViewModel allReportMainViewModel = await _reportService.FilteredReports(searchTerm,page);

            return PartialView("_AllFilteredReportsPartial",allReportMainViewModel);
        }
        
    }
}
