using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.ViewModels
{
    public class EquipmentDetailsViewModel
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public string Manufacturer { get;set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public int Warranty { get; set; }

        public int LifeSpan { get; set; }

        public int LifeSpanYears { get; set; }

        public string AddedFrom { get; set; }

        public string AssetTag { get; set; }

        public string Category { get; set; }
        public string CreatedAt { get; set; }
        public string CurrentStatus { get; set; }
        public string Location { get; set; }

        public string Notes{ get; set; }


        public string CustomProperties { get; set; }

        public string? ImageUrl { get; set; }
    }
}
