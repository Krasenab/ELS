using Els.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
    public class FilteredTicketsViewModel
    {
        public string? SearchTerm { get; set; }
        public string? Status { get; set; }
        public List<TicketStatus> Statuses { get; set; }
        public string? AppUserId { get; set; }
        public string? Priority { get; set; }
        public List<TicketPriority> Priorities { get; set; }     

        public List<AllTicketsViewModel> AllTickets { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; } 


    }
}
