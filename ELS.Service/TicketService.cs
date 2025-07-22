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

        public async Task CreateTicketAsync(CreateTicketViewModel inputModel)
        {
            Ticket ticket = new Ticket()
            {
                Title = inputModel.Title,
                Priority = inputModel.Priority,
                Description = inputModel.Description,
                CreatedAt = DateTime.UtcNow.Date
                
            };
         
           await _dbContext.Tickets.AddAsync(ticket);
           await _dbContext.SaveChangesAsync();
        
        }

        public async Task<List<AllTicketsViewModel>> FilteredAllTicketsAsync(string searchTerm, string status,string priority)
        {
            var ticketsQuery = _dbContext.Tickets.AsQueryable();
            List<AllTicketsViewModel> allTickets = new List<AllTicketsViewModel>();
            if (!String.IsNullOrEmpty(searchTerm) || !string.IsNullOrWhiteSpace(searchTerm)) 
            {
               ticketsQuery.Where(t =>
                
                   EF.Functions.Like(t.Title,searchTerm)||
                   EF.Functions.Like(t.Technician.AppUser.FirstName,searchTerm)
                   
                );
            }

            if (!String.IsNullOrEmpty(status) || !String.IsNullOrWhiteSpace(status)) 
            {
                ticketsQuery.Where(t =>
                    EF.Functions.Like(t.Status,status)

                );
            }
            if (!String.IsNullOrEmpty(priority) || !String.IsNullOrWhiteSpace(priority)) 
            {
                ticketsQuery.Where( t=> 
                 EF.Functions.Like(t.Priority,priority)
                    );
            }

            allTickets = await ticketsQuery.Select(t=> new AllTicketsViewModel() 
            {
                Title = t.Title,
                CreatedOn = t.CreatedAt.ToString(),
                Technician = t.Technician.AppUser.FirstName,
                Prioroty = t.Priority,
                Status = t.Status,
                EquipmentName  = t.Equipment.EquipmentName

            }).ToListAsync();
           return allTickets;
        }

        public async Task<List<AllTicketsViewModel>> GetAllTicketsAsync()
        {
            return await _dbContext.Tickets.Select(t => new AllTicketsViewModel
            {
                Id = t.TicketId.ToString(),
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

        public List<TicketStatus> GetStatuses()
        {
            return Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().ToList();
        }


    }
}
