using ELS.Service.Interfaces;
using ELS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELS.Controllers
{
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private IEquipmentService _equipmentService;
        public TicketController(ITicketService ticketService,IEquipmentService equipmentService)
        {
            this._ticketService = ticketService;
            this._equipmentService = equipmentService;
        }
        [HttpGet]
        public IActionResult Create(string equipmentId)
        {
            
            CreateTicketViewModel viewModel = new CreateTicketViewModel()
            {
                Priorities = _ticketService.GetPriorities(),
                EquipmentId = equipmentId,
                
            };
            
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketViewModel inputViewModel) 
        {
            await _ticketService.CreateTicketAsync(inputViewModel);
            TempData["SuccessMessage"] = "Ticket was been created";
            return RedirectToAction("All","Ticket");
        }

        [HttpGet]
        public async Task<IActionResult> AllTickets() 
        {
            FilteredTicketsViewModel viewModel = new FilteredTicketsViewModel()
            {
                AllTickets = await _ticketService.GetAllTicketsAsync(),
                Priorities = _ticketService.GetPriorities(),               
            };

            return View(viewModel);
        }


    }
}
