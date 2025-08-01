﻿using Els.ViewModels.Enums;
using ELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service.Interfaces
{
    public interface ITicketService
    {
        public Task<bool> IsTiecketExistByIdAsync(string ticketId);
        public Task ChangeStatusAsync(string ticketId,string status);
        public Task SetTechnicianAsync(string ticketId,string technicianId);
        public Task CreateTicketAsync(CreateTicketViewModel inputModel);
        public Task<List<AllTicketsViewModel>> GetAllTicketsAsync();
        public Task<List<AllTicketsViewModel>> FilteredAllTicketsAsync(string searchTerm, string status,string priority);
        public Task<TicketDetailsViewModel> GetTicketDetailsAsync(string ticketId);
        public List<TicketPriority> GetPriorities();     
        public List<TicketStatus> GetStatuses();    
    }
}
