using Els.ViewModels.Enums;
using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service
{
    public class TicketService : ITicketService
    {
        private ElsDbContext _dbContext;
        public TicketService(ElsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task CreateTicketAsync(CreateTicketViewModel inputModel)
        {
            Ticket ticket = new Ticket()
            {
                Title = inputModel.Title,
                Priority = inputModel.Priority,
                Description = inputModel.Description,
                CreatedAt = DateTime.UtcNow.Date
                
            };
            throw new NotImplementedException();
        }

        public async Task<List<AllTicketsViewModel>> GetAllTicketsAsync()
        {
            return await _dbContext.Tickets.Select(t => new AllTicketsViewModel
            {
                Title = t.Title,
                EquipmentName = t.Equipment.EquipmentName,
                CreatedOn = t.CreatedAt.ToString(),
                Status = t.Status,
                Prioroty = t.Priority,
                Technician = t.Technician.AppUser.FirstName
            }).ToListAsync();
        
        
        }

        public List<TicketPriority> GetPriorities()
        {
           return Enum.GetValues(typeof(TicketPriority)).Cast<TicketPriority>().ToList();
        }
    }
}
