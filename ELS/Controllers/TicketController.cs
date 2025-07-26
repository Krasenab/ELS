using ELS.Service.Interfaces;
using ELS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static ELS.Infrastrucure.ClaimsPrincipalExtensions;
namespace ELS.Controllers
{
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private IAppUserService _appUserService;
        public TicketController(ITicketService ticketService,IAppUserService appUserService)
        {
            this._ticketService = ticketService;
            this._appUserService = appUserService;
        }
        [HttpGet]
        public IActionResult Create(string equipmentId)
        {
            
            CreateTicketViewModel viewModel = new CreateTicketViewModel()
            {
                Priorities = _ticketService.GetPriorities(),
                EquipmentId = equipmentId,
                Statuses = _ticketService.GetStatuses()
                
            };
            
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketViewModel inputViewModel) 
        {
            inputViewModel.ApplicantId = getCurrentUserId(this.User);
            
            await _ticketService.CreateTicketAsync(inputViewModel);
            TempData["SuccessMessage"] = "Ticket was been created";
            return RedirectToAction("All","Ticket");
        }

        [HttpGet]
        public async Task<IActionResult> AllTickets() 
        {
            FilteredTicketsViewModel viewModel = new FilteredTicketsViewModel()
            {
                SearchTerm = "",
                Status = "",
                Priority = "",
                AllTickets = await _ticketService.GetAllTicketsAsync(),
                Priorities = _ticketService.GetPriorities(),
                Statuses = _ticketService.GetStatuses(),
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> AllFilteredTickets(string searchTerm,string status,string priority) 
        {
            List<AllTicketsViewModel> filteredView = await _ticketService.FilteredAllTicketsAsync(searchTerm, status, priority);
            return PartialView(filteredView);
        }

        [HttpGet]
        public async Task<IActionResult> TicketDetails(string ticketId) 
        {
            TicketDetailsViewModel viewModel = await _ticketService.GetTicketDetailsAsync(ticketId);
            viewModel.Statuses = _ticketService.GetStatuses();

            return View(viewModel);
        }

        


    }
}
