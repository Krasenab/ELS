using Els.ViewModels.Enums;
using ELS.ViewModels;

namespace ELS.Service.Interfaces
{
    public interface IEquipmentService
    {
        public Task AddEquipmentAsync(AddEquipmentViewModel viewModel);
        public Task<List<AllEquipmentViewModel>> GetAllEquipmentAsync();
        public List<EquipmentStatus> GetEuqipmentStatues(); 

        public List<EditE>
        
    }
}
