using Els.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
    public class CreateTicketViewModel
    {
        public CreateTicketViewModel()
        {
            
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Priority { get; set; }

        public List<TicketPriority> Priorities { get; set; }

        public string EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string? ApplicantName { get; set; }
        public string ApplicantId { get; set; }
        public string? ApplicantContacts { get; set; }
        public string Status { get; set; }
        public List<TicketStatus> Statuses { get; set; }

        [Required]
        public string Description { get; set; }



    }
}
