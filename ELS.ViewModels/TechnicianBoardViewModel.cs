using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
    public class TechnicianBoardViewModel
    {
        public TechnicianBoardViewModel()
        {
            this.TechnicianTikets = new List<TicketDetailsViewModel>();
            this.Reports = new List<ReportDetailViewModel>();
        }
       public string? TechnicianId { get; set; }
        public int TotalOpen { get; set; }
        public int TotalInProgress { get; set; }
        public int TotalClosed { get; set; }        
        public List<TicketDetailsViewModel> TechnicianTikets { get; set; }

        public List<ReportDetailViewModel> Reports { get; set; }   
    }
}
