using Els.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
    public class TicketDetailsViewModel
    {
        public  string  Id { get; set; }
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public string EquipmentName { get; set; }
        public string Description { get; set; }
        public string? Technician { get; set; }
        public string? CurrentUserId { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public List<TicketStatus> Statuses { get; set; }
    }
}
