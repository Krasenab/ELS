using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Els.Constants.EntityValidationsConstants;

namespace ElsModels.SQL
{
    public class AppUser: IdentityUser<Guid>
    {
        public AppUser()
        {
            this.Tickets = new List<Ticket>();
        }

        [Required]
        [MaxLength(ApplicationUserNameMaxLenght)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(ApplicationUserLastNameMaxLength)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(FirmIdMaxLength)]
        public string FirmId { get; set; }
        [Required]
        [MaxLength(DepartmentMaxLength)]
        public string Department { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

    }
}
