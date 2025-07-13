using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
    public class FilteredEquipmentViewModel
    {
        public string? SearchTerm { get; set; }

        public string? CategoryFilter { get; set; }

        public string? StatusFilter { get; set; }

       // public List<string> Status { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<AllEquipmentViewModel> AllEquipment { get; set; }

        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }

    }
}
