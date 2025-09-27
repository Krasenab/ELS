using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels.RportsVIewModels
{
     public class AllReportMainViewModel
    {
        public string? SearchTerm { get; set; } = "";
        public int? PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public List<AllReportsVIewModel> Reports { get; set; }



    }
}
