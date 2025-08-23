using ELS.Infrastrucure;
using ELS.Service.Interfaces;
using ELS.ViewModels;
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
    }
}
