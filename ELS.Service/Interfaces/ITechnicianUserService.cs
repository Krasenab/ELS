using ELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service.Interfaces
{
    public interface ITechnicianUserService
    {
        public Task<string> GetTechnicianIdAsync(string applicationUserId);
        public Task RegisterAsTechnician(RegisterAsTechnicianViewModel viewMode);
        public Task<bool> IsTechnicianExistByUserIdAsync(string appUserId);

       
        
    }
}
