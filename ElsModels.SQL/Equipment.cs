
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Els.Constants.EntityValidationsConstants;

namespace ElsModels.SQL
{
    public class Equipment
    {
        public Equipment()
        {
            this.Images = new List<Image>();
            this.Tickets = new List<Ticket>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(EquipmentNameMaxLength)]
        public string EquipmentName { get; set; }

        [Required]
        [MaxLength(EquipmentStatusMaxLength)]
        public string CurrentStatus { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(EquipmentSerialNumberMaxLength)]
        public string SerialNumber { get; set; }

        public int EquipmentWarrantyMonths { get; set; }
        [Required]
        [MaxLength(EquipmentManufacturerMaxLength)]
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

        [Required]
        public string AddedFrom { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Ticket> Tickets { get; set; }
        public List<Image> Images { get; set; }


    }
}
