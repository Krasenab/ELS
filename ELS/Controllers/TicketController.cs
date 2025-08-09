using ELS.Infrastrucure;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
using Microsoft.AspNetCore.Mvc;
using static ELS.Infrastrucure.ClaimsPrincipalExtensions;
namespace ELS.Controllers
{
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private IAppUserService _appUserService;
        private ITechnicianUserService _technicianUserService;
        public TicketController(ITicketService ticketService,IAppUserService appUserService,ITechnicianUserService technicianUserService)
        {
            this._ticketService = ticketService;
            this._appUserService = appUserService;
            this._technicianUserService = technicianUserService;
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
            return PartialView("_AllFilteredTicketsPartial",filteredView);
        }

        [HttpGet]
        public async Task<IActionResult> TicketDetails(string id) 
        {
            string currentUserId = ClaimsPrincipalExtensions.getCurrentUserId(this.User);
            TicketDetailsViewModel viewModel = await _ticketService.GetTicketDetailsAsync(id);
            if (viewModel==null)
            {
                TempData["ErrorMessage"] = "Something get wrong";

                return RedirectToAction("AllTickets", "Ticket");
            }
            viewModel.CurrentUserId = currentUserId;
            viewModel.Statuses = _ticketService.GetStatuses();

            return View(viewModel);
        } 

        [HttpPost]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeTicketStatusViewModel dtoModel) 
        {
            await _ticketService.ChangeStatusAsync(dtoModel.ticketId, dtoModel.ticketStatus);
            TempData["SuccessMessage"] = "Ticket status has been changed";
            return Json(new { success = true, message = TempData["SuccessMessage"] });

        }
        [HttpPost]
        public async Task<IActionResult> TakeTicket([FromBody] TakeTicketViewModel model) 
        {
            string techId = await _technicianUserService.GetTechnicianIdAsync(model.appUserId);
            bool isOwnerExist = await _ticketService.CheckForOwnerAsync(model.ticketId);
            if (isOwnerExist)
            {
                return Json(new
                {
                    success = false,
                    message = "This ticket cannot be accepted by you"
                });

            }

            await _ticketService.SetTechnicianAsync(model.ticketId, techId);
          
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [HttpGet]
        public async Task<IActionResult> MyTicketDetails(string id) 
        {
            TicketDetailsViewModel ticket =  await _ticketService.GetTicketDetailsAsync(id);

            ticket.Statuses = _ticketService.GetStatuses();
            

            return View(ticket);
        }

    }
}
