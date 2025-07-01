using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Els.Constants.EntityValidationsConstants;

namespace ElsModels.SQL
{
    public class Technician
    {
        public Technician()
        {
            this.Tickets = new List<Ticket>();
            this.Reports = new List<Report>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(FirmIdMaxLength)]
        public string FirmId { get; set; }
        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(AppUserId))]
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public virtual List<Report> Reports { get; set; }
        public virtual List<Ticket> Tickets { get; set; }


    }
}
