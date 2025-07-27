using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELS.Service
{
    public class TechnicianUserService : ITechnicianUserService
    {
        
        private ElsDbContext _dbContext;
        public TechnicianUserService(ElsDbContext dbContext)
        {
            this._dbContext=dbContext;
        }
        public async Task RegisterAsTechnician(RegistierAsTechnicianViewModel viewMode)
        {
            Technician technician = new Technician() 
            {
                FirmId = viewMode.FirmId,
                PhoneNumber = viewMode.PhoneNumber,
                AppUserId = Guid.Parse(viewMode.AppUserId)                
            };

            await _dbContext.Technicians.AddAsync(technician);
            await _dbContext.SaveChangesAsync();



        }
    }
}
