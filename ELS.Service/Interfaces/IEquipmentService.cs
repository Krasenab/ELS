using Els.ViewModels.Enums;
using ELS.ViewModels;

namespace ELS.Service.Interfaces
{
    public interface IEquipmentService
    {
        public Task RemoveEquipmentAsync(string equipmentId);
        public Task AddEquipmentAsync(AddEquipmentViewModel viewModel);
        public Task<List<AllEquipmentViewModel>> GetAllEquipmentAsync();
        public List<EquipmentStatus> GetEuqipmentStatuses(); 
        public Task EditEquipmentAsync(EditEquipmentViewModel viewModel);
        public Task<EditEquipmentViewModel> GetEquipmetForEditByIdAsync(string id);
        public Task<List<AllEquipmentViewModel>> GetAllFilteredEquipmentAsync(string searchTerm,string category,string status);
         

    }
}
