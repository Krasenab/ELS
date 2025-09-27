using Els.ViewModels.Enums;
using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ELS.ViewModels.TichnicianViewModels;
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
            TicketDetailsViewModel? ticket = await _dbContext.Tickets.Where(t => t.TicketId.ToString() == ticketId).Select(td=> new TicketDetailsViewModel 
                   {
                       Technician = td.Technician.FirmId,
                   }
                ).FirstOrDefaultAsync();

            if (ticket.Technician == null)
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

        public async Task<FilteredTicketsViewModel> FilteredAllTicketsAsync(string searchTerm, string status,string priority, int page = 1, int pageSize = 5)
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
                 EF.Functions.Like(t.Equipment.EquipmentName,pattern)||
                 EF.Functions.Like(t.Description,pattern)
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
            int totalCount = await ticketsQuery.CountAsync();
            allTickets = await ticketsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t=> new AllTicketsViewModel() 
            {
                Id = t.TicketId.ToString(),
                Title = t.Title,
                CreatedOn = t.CreatedAt.ToString(),
                Technician = t.Technician.AppUser.FirstName,
                Priority = t.Priority,
                Status = t.Status,
                EquipmentName  = t.Equipment.EquipmentName

            }).ToListAsync();
           
        

            FilteredTicketsViewModel result = new FilteredTicketsViewModel()
            {
                AllTickets = allTickets,
                PageNumber = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                SearchTerm = searchTerm,
                Status = status,
                Priority = priority
            };
            return result;
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

        public async Task<int> GetFilteredTicketTotalCount(string searchTerm, string status, string priority)
        {
            var query = _dbContext.Tickets.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var pattern = $"%{searchTerm.Trim()}%";
                query = query.Where(t =>
                  EF.Functions.Like(t.Title, pattern) ||
                  EF.Functions.Like(t.CreatedAt.ToString(),pattern) 
                
                );
            }

            if (!string.IsNullOrWhiteSpace(status)) {
                query = query.Where(t => t.Status == status);
            }
              

            if (!string.IsNullOrWhiteSpace(priority))
                query = query.Where(t => t.Priority == priority);

            return await query.CountAsync();
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
                    Technician =t.Technician.AppUser.FirstName,
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
        // trqbva da se iztire tova ako ne raboti 
        public async Task<PagedMyTicketsViewModel> PagedAndFilteredMyTickets(string technicianId, string searchTerm, int page, int pageSize)
        {
            var query = _dbContext.Tickets.AsNoTracking().Include(e => e.Equipment).Where(x => x.TechnicianId.ToString() == technicianId).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                string search = $"{searchTerm.Trim()}%";
                query = query.Where(t => EF.Functions.Like(t.Title, search) ||
                EF.Functions.Like(t.Description,search) ||
                EF.Functions.Like(t.Priority,search) ||
                EF.Functions.Like(t.Status,search));
            }

            int totalItems = query.Count();

            var pagedTickets = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TicketDetailsViewModel
            {
                Id = t.TicketId.ToString(),
                Title = t.Title,
                EquipmentName = t.Equipment.EquipmentName,
                Status = t.Status,
                Priority = t.Priority
            })
            .ToListAsync();

            

            return  new PagedMyTicketsViewModel
            {
                TechnicianId = technicianId,
                Tickets = await pagedTickets,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                Search = searchTerm
            };

        }

        public async Task SetTechnicianAsync(string ticketId, string technicianId)
        {
            Ticket? ticket = await _dbContext.Tickets.Where(tid => tid.TicketId.ToString() == ticketId).FirstOrDefaultAsync();
            ticket.TechnicianId = Guid.Parse(technicianId);
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
