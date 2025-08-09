using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
using Microsoft.EntityFrameworkCore;
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

        

        public async Task<string> GetTechnicianIdAsync(string applicationUserId)
        {
            string userId = applicationUserId.ToUpper();
            return await _dbContext.Technicians.Where(id=>id.AppUserId.ToString()==applicationUserId).Select(id=> id.Id.ToString()).FirstOrDefaultAsync();
        }

        public async Task<bool> IsTechnicianExistByUserIdAsync(string appUserId)
        {
            bool isExist = await _dbContext.Technicians.Where(t=>t.AppUserId.ToString()==appUserId).AnyAsync();
            if (!isExist)
            {
                return false;
            }
            return true;
        }

        public async Task RegisterAsTechnician(RegisterAsTechnicianViewModel viewMode)
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
