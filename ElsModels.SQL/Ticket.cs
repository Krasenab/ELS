using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static Els.Constants.EntityValidationsConstants;

namespace ElsModels.SQL
{
    public class Ticket
    {
        public Ticket()
        {
            this.Images = new List<Image>();
        }
        [Key]
        public Guid TicketId { get; set; }

        [Required]
        [MaxLength(TicketTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(TicketPriorityMaxLength)]
        public string Priority { get; set; }

        [Required]
        [MaxLength(TicketStatusMaxLength)]
        public string Status { get; set; }

        [Required]
        [ForeignKey(nameof(EquipmentId))]
        public Guid EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }

        [Required]
        [ForeignKey(nameof(AppUserId))]
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [ForeignKey(nameof(TechnicianId))]
        public Guid? TechnicianId { get; set; }
        public virtual Technician Technician { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public virtual List<Image> Images { get; set; }

    }
}
