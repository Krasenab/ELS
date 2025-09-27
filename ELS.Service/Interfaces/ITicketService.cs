using Els.ViewModels.Enums;
using ELS.ViewModels;
using ELS.ViewModels.TichnicianViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service.Interfaces
{
    public interface ITicketService
    {
        public Task<PagedMyTicketsViewModel> PagedAndFilteredMyTickets(string technicianId, string searchTerm, int page, int pageSize);
        public Task<int> GetFilteredTicketTotalCount(string searchTerm, string status, string priority);
        public Task DeleteAllEquipmentTicketsAsync(string equipmentId);
        public Task<bool> CheckForOwnerAsync(string ticketId);
        public Task<List<TicketDetailsViewModel>> GetTIcketsByTechnicianIdAsync(string technicianId);
        public Task<bool> IsTiecketExistByIdAsync(string ticketId);
        public Task ChangeStatusAsync(string ticketId,string status);
        public Task SetTechnicianAsync(string ticketId,string technicianId);
        public Task CreateTicketAsync(CreateTicketViewModel inputModel);
        public Task<List<AllTicketsViewModel>> GetAllTicketsAsync();
        public Task<FilteredTicketsViewModel> FilteredAllTicketsAsync(string searchTerm, string status,string priority, int page, int pageSize);
        public Task<TicketDetailsViewModel> GetTicketDetailsAsync(string ticketId);
        public List<TicketPriority> GetPriorities();     
        public List<TicketStatus> GetStatuses();    
    }
}
