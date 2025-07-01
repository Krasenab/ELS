using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsModels.SQL
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UniqueName { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public Guid? EquipmentId { get; set; }
        public virtual Equipment? Equipment { get; set; }

        [ForeignKey(nameof(AppUserId))]
        public Guid? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey(nameof(TicketId))]
        public Guid? TicketId { get; set; }
        public virtual Ticket? Ticket { get; set; }

    }
}
