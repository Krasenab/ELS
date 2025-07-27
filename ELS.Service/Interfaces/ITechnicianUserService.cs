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
        public Task RegisterAsTechnician(RegistierAsTechnicianViewModel viewMode);
    }
}
