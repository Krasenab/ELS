using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;

namespace ELS.Service
{
    public class EquipmentService : IEquipmentService
    {
        public ElsDbContext _dbContext;

        public EquipmentService(ElsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public Task AddEquipmentAsync(AddEquipmentViewModel viewModel)
        {
            Equipment equipment = new Equipment()
            {
                EquipmentName = viewModel.EquipmentName,
                AddedFrom = viewModel.AddedFrom,
                CurrentStatus = viewModel.CurrentStatus,
                CustomProperties = viewModel.CustomProperties,
                AssetTag = viewModel.AssetTag,
                UpdatedAt = viewModel.UpdatedAt,
                LifeSpanYears = viewModel.LifeSpanYears,
                Description = viewModel.Description,
                CreatedAt = viewModel.CreatedAt,
                Notes = viewModel.Notes,
                Manufacturer = viewModel.Manufacturer,
                SerialNumber = viewModel.SerialNumber,
                Location = viewModel.Location,
               EquipmentWarrantyMonths = viewModel.EquipmentWarrantyMonths,
               Model = viewModel.Model,
               
            };
            throw new NotImplementedException();
        }
    }
}
