using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;

namespace ELS.Service
{
    public class EquipmentService : IEquipmentService
    {
        public ElsDbContext _dbContext;

        public EquipmentService(ElsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddEquipmentAsync(AddEquipmentViewModel viewModel)
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
                CategoryId = viewModel.CategoryId,
               EquipmentWarrantyMonths = viewModel.EquipmentWarrantyMonths,
               Model = viewModel.Model,
                 
            };

            equipment.CreatedAt = DateTime.UtcNow;
           await _dbContext.Equipments.AddAsync(equipment);
           await _dbContext.SaveChangesAsync();
        }

        public Task<List<AllEquipmentViewModel>> GetAllEquipmentAsync()
        {
            return _dbContext.Equipments.Select(e => new AllEquipmentViewModel()
            {
                Id = e.Id.ToString(),
                EquipmentName = e.EquipmentName,
                SerialNumber = e.SerialNumber,
                AddedFrom = e.AddedFrom,
                CreatedAt = e.CreatedAt,
                CurrentStatus = e.CurrentStatus,
                Category = e.Category.Name,
                LifeSpanYears = e.LifeSpanYears,
                AssetTag = e.AssetTag,
                Location = e.Location,
                Manufacturer = e.Manufacturer,
                Model = e.Model,
                Description = e.Description,
                EquipmentWarrantyMonths = e.EquipmentWarrantyMonths,
                CustomProperties = e.CustomProperties,
                Notes = e.Notes,
               
            }).ToListAsync();
        }
    }
}
