using Els.ViewModels.Enums;
using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task EditEquipmentAsync(EditEquipmentViewModel viewModel)
        {
            Equipment? e = await _dbContext.Equipments.Where(e=>e.Id.ToString()==viewModel.Id).FirstOrDefaultAsync();

            
            e.EquipmentName = viewModel.EquipmentName;
            e.CurrentStatus = viewModel.CurrentStatus;
            e.AddedFrom = viewModel.AddedFrom;
            e.SerialNumber = viewModel.SerialNumber;
            e.LifeSpanYears = viewModel.LifeSpanYears;
            e.Model = viewModel.Model;
            e.CustomProperties = viewModel.CustomProperties;
            e.AssetTag = viewModel.AssetTag;
            e.CreatedAt = DateTime.UtcNow;
            e.Notes = viewModel.Notes;
            e.Description = viewModel.Description;
            e.Location = viewModel.Location;
            e.CategoryId = viewModel.CategoryId;
            e.Manufacturer = viewModel.Manufacturer;
            e.EquipmentWarrantyMonths = viewModel.EquipmentWarrantyMonths;

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
                Notes = e.Notes

            }).ToListAsync();
        }

        public async Task<List<AllEquipmentViewModel>> GetAllFilteredEquipmentAsync(string searchTerm, string category,string status)
        {
            List<AllEquipmentViewModel> result = new List<AllEquipmentViewModel>();

            var query = _dbContext.Equipments.Include(c=>c.Category).AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm) || !string.IsNullOrWhiteSpace(searchTerm)) 
            {
                string serchTerm = searchTerm.Trim() + "%";
                query = query.Where(x =>
                    EF.Functions.Like(x.SerialNumber,serchTerm)||
                    EF.Functions.Like(x.EquipmentName,serchTerm)||
                    EF.Functions.Like(x.Manufacturer,serchTerm)||
                    EF.Functions.Like(x.AssetTag,serchTerm)||
                    EF.Functions.Like(x.AddedFrom,serchTerm)||
                    EF.Functions.Like(x.LifeSpanYears.ToString(),serchTerm)
                );
            }

            if (!string.IsNullOrEmpty(category)) 
            {
               

                query = query.Where(ec =>
                    
                    EF.Functions.Like(ec.Category.Name,category)

                );
            }

            if (!string.IsNullOrEmpty(status) || !string.IsNullOrWhiteSpace(status)) 
            {
                
                query = query.Where(es =>
                    EF.Functions.Like(es.CurrentStatus,status)
                    
                );
            }

            result = await query.Select(e => new AllEquipmentViewModel()
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
                Notes = e.Notes
            }).ToListAsync();
            
            return result;
        }

        public async Task<EditEquipmentViewModel> GetEquipmetForEditByIdAsync(string id)
        {
           EditEquipmentViewModel? equipment = await _dbContext.Equipments.Where(e=>e.Id.ToString()==id).Select(e => new EditEquipmentViewModel() 
           {
               Id = e.Id.ToString(),
               EquipmentName = e.EquipmentName,
               SerialNumber = e.SerialNumber,
               AddedFrom = e.AddedFrom,
               CurrentStatus = e.CurrentStatus,
               AssetTag= e.AssetTag,
               Location = e.Location,
               Manufacturer = e.Manufacturer,
               Model = e.Model,
               Description = e.Description,
               CreatedAt = e.CreatedAt,
               Notes =e.Notes,
               LifeSpanYears = e.LifeSpanYears,
               EquipmentWarrantyMonths=e.EquipmentWarrantyMonths,
               CustomProperties = e.CustomProperties,
               
           }).FirstOrDefaultAsync();

            return equipment;
        }

        public List<EquipmentStatus> GetEuqipmentStatuses()
        {
            return Enum.GetValues(typeof(EquipmentStatus)).Cast<EquipmentStatus>().ToList();
        } 
    }
}
