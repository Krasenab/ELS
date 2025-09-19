using Els.ViewModels.Enums;
using ELS.ViewModels;

namespace ELS.Service.Interfaces
{
    public interface IEquipmentService
    {
        public Task<EquipmentDetailsViewModel> GetEquipmentAsync(string equipmentId);
        public Task RemoveEquipmentAsync(string equipmentId);
        public Task AddEquipmentAsync(AddEquipmentViewModel viewModel);
        public Task<List<AllEquipmentViewModel>> GetAllEquipmentAsync();
        public List<EquipmentStatus> GetEuqipmentStatuses(); 
        public Task EditEquipmentAsync(EditEquipmentViewModel viewModel);
        public Task<EditEquipmentViewModel> GetEquipmetForEditByIdAsync(string id);
        public Task<FilteredEquipmentViewModel> GetAllFilteredEquipmentAsync(string searchTerm,string category,string status,int page);
    }
}
