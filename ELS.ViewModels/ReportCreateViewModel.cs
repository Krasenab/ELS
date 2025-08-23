using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
    public class ReportCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string TicketId { get; set; }
        public IFormFile? Attachment { get; set; }
        [Required]
        public string TechnicianId { get; set; }
    }
}
