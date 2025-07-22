using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
   public class AllTicketsViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string EquipmentName { get; set; }

        public string Prioroty  { get; set; }

        public string Status { get; set; }

        public string CreatedOn { get; set; }

        public string? Technician { get; set; }


    }
}
