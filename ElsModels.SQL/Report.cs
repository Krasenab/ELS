using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsModels.SQL
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ReportSerialNumber { get; set; }

        [Required]
        [ForeignKey(nameof(TicketId))]
        public Guid TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        [Required]
        [ForeignKey(nameof(TechnicianId))]
        public Guid TechnicianId { get; set; }
        public Technician Technician { get; set; }
        [Required]
        public string WorkDescription { get; set; }
        //public decimal MaterialCost { get; set; }
        //public string? UsedParts { get; set; }

    }
}
