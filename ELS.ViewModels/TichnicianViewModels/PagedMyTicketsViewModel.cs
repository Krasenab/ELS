using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels.TichnicianViewModels
{
    public class PagedMyTicketsViewModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => (int)System.Math.Ceiling((double)TotalItems / PageSize);

        public string Search { get; set; }

        public string TechnicianId { get; set; }

        public List<TicketDetailsViewModel> Tickets { get; set; }
    }
}
