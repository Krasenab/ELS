using Els.ViewModels.Enums;
using System.ComponentModel.DataAnnotations;

using static Els.Constants.EntityValidationsConstants;
namespace ELS.ViewModels
{
    public class EditEquipmentViewModel
    {

        public string Id { get; set; }

        [Required]
        [MaxLength(EquipmentNameMaxLength)]
        [MinLength(EquipmentNameMinLength)]
        public string EquipmentName { get; set; }


        [Required]
        public string CurrentStatus { get; set; }
        public IEnumerable<EquipmentStatus> EquipmentStatuses { get; set; }


        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(EquipmentSerialNumberMaxLength)]
        [MinLength(EquipmentSerialNumberMinLength)]
        public string SerialNumber { get; set; }

        [Required]
        public string AddedFrom { get; set; }

        public int EquipmentWarrantyMonths { get; set; }
        [Required]
        [MaxLength(EquipmentManufacturerMaxLength)]
        [MinLength(EquipmentSerialNumberMinLength)]
        public string Manufacturer { get; set; }
        [Required]
        [MaxLength(EquipmentModelMaxLength)]
        public string Model { get; set; }
        [MaxLength(EquipmentAssetTagMaxLength)]
        public string? AssetTag { get; set; }

        public string? Location { get; set; }
        public string? CustomProperties { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int LifeSpanYears { get; set; }
        public string? Notes { get; set; }
        public int CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<CategoryViewModel> Categories { get; set; }

    }
}
