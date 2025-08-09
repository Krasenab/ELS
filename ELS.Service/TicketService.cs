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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ELS.Service
{
    public class TicketService : ITicketService
    {
        private ElsDbContext _dbContext;
        public TicketService(ElsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task ChangeStatusAsync(string ticketId, string status)
        {
            Ticket? t = await _dbContext.Tickets.Where(tid=>tid.TicketId.ToString() == ticketId).FirstOrDefaultAsync();
            t.Status = status;           
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckForOwnerAsync(string ticketId)
        {
            bool isExistOwenr = await _dbContext.Tickets.Where(x => x.TicketId.ToString() == ticketId).Select(s => s.TechnicianId == null).AnyAsync();
            if (isExistOwenr)
            {
                return false;
            }
            return true;
        }

        public async Task CreateTicketAsync(CreateTicketViewModel inputModel)
        {
            Ticket ticket = new Ticket()
            {
                Title = inputModel.Title,
                Priority = inputModel.Priority,
                Description = inputModel.Description,
                CreatedAt = DateTime.UtcNow.Date,
                Status = inputModel.Status,
                AppUserId = Guid.Parse(inputModel.ApplicantId),
                EquipmentId = Guid.Parse(inputModel.EquipmentId)

            };
         
           await _dbContext.Tickets.AddAsync(ticket);
           await _dbContext.SaveChangesAsync();
        
        }

        public async Task DeleteAllEquipmentTicketsAsync(string equipmentId)
        {
            
           List<Ticket> tickets =  await _dbContext.Tickets.Where(e=>e.EquipmentId.ToString()==equipmentId).ToListAsync();
            foreach (Ticket ticket in tickets) 
            {
                _dbContext.Tickets.Remove(ticket);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<AllTicketsViewModel>> FilteredAllTicketsAsync(string searchTerm, string status,string priority)
        {

            var ticketsQuery = _dbContext.Tickets
                .Include(e => e.Technician)
                .ThenInclude(t => t.AppUser)
                .Include(e => e.Equipment)
                .AsQueryable();
            List<AllTicketsViewModel> allTickets = new List<AllTicketsViewModel>();
            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                var pattern = $"%{searchTerm.Trim()}%";

                ticketsQuery = ticketsQuery.Where(t =>
                 EF.Functions.Like(t.Title, pattern) ||
                 (t.Technician != null && t.Technician.AppUser != null &&
                 EF.Functions.Like(t.Technician.AppUser.FirstName ?? "", pattern))
                     );
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                ticketsQuery = ticketsQuery.Where(t =>
                    t.Status == status
                );
            }

            if (!string.IsNullOrWhiteSpace(priority))
            {
                ticketsQuery = ticketsQuery.Where(t =>
                    t.Priority == priority
                );
            }

            allTickets = await ticketsQuery.Select(t=> new AllTicketsViewModel() 
            {
                Id = t.TicketId.ToString(),
                Title = t.Title,
                CreatedOn = t.CreatedAt.ToString(),
                Technician = t.Technician != null && t.Technician.AppUser != null
                        ? t.Technician.AppUser.FirstName
                        : "—",
                Priority = t.Priority,
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
                Priority = t.Priority,
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

        public async Task<TicketDetailsViewModel> GetTicketDetailsAsync(string ticketId)
        {
            TicketDetailsViewModel? ticket = await _dbContext.Tickets.Where(tid=>tid.TicketId.ToString() == ticketId)
                .Select(t=>new TicketDetailsViewModel() 
                {
                    Id = t.TicketId.ToString(),
                    Title = t.Title,
                    EquipmentName = t.Equipment.EquipmentName,
                    Description = t.Description,
                    Technician = t.Technician.AppUser.FirstName ?? "Not Accepted Ticket",
                    CreatedAt = t.CreatedAt.ToString("dd-MM-yyyy"),
                    Priority = t.Priority,
                    Status = t.Status

                })
                .FirstOrDefaultAsync();
            return ticket;
        }

        public async Task<List<TicketDetailsViewModel>> GetTIcketsByTechnicianIdAsync(string technicianId)
        {
            List<TicketDetailsViewModel> ticketDetailsViewModel = await _dbContext.Tickets.Where(techId => techId.TechnicianId.ToString() == technicianId).Select(t => new TicketDetailsViewModel()
            {
                Id = t.TicketId.ToString(),
                Title = t.Title,
                Priority = t.Priority,
                Description = t.Description,
                Status = t.Status,
                EquipmentName = t.Equipment.EquipmentName,
                CreatedAt = t.CreatedAt.ToString()
            }).ToListAsync();
            return ticketDetailsViewModel;
        }

        public async Task<bool> IsTiecketExistByIdAsync(string ticketId)
        {
            bool isExist = await _dbContext.Tickets.Where(tid=>tid.TicketId.ToString()==ticketId).AnyAsync();
            if (!isExist)
            {
                return false;
            }

            return true;

        }

        public async Task SetTechnicianAsync(string ticketId, string technicianId)
        {
            Ticket? ticket = await _dbContext.Tickets.Where(tid => tid.TicketId.ToString() == ticketId).FirstOrDefaultAsync();
            ticket.TechnicianId = Guid.Parse(technicianId);
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
