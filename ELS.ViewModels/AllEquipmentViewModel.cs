using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
    public class AllEquipmentViewModel
    {
        public string Id { get; set; }
        public string EquipmentName { get; set; }

        public string CurrentStatus { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string AddedFrom { get; set; }

        public int EquipmentWarrantyMonths { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string? AssetTag { get; set; }
        public string? Location { get; set; }
        public string? CustomProperties { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int LifeSpanYears { get; set; }
        public string? Notes { get; set; }
       public string Category { get; set; }
    }
}
