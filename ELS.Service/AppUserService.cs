using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ELS.Service
{
    public class AppUserService:IAppUserService
    {
        private ElsDbContext _dbContext;
        public AppUserService(ElsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AppUserViewModel> GetAppUserAsync(string appUserId)
        {
            AppUserViewModel? appUser = await _dbContext.AppUsers.Where(id => id.Id.ToString() == appUserId)
                .Select(appU => new AppUserViewModel()
                {
                    Id = appUserId,
                    Email = appU.Email,
                    Department = appU.Department,
                    FirmId = appU.FirmId,
                    FirstName = appU.FirstName,
                    LastName = appU.LastName,
                    UserName = appU.UserName
                }).FirstOrDefaultAsync();

            return appUser;
        }

        public string UserEmail(string firmId)
        {
            string? result = _dbContext.AppUsers.Where(x=>x.FirmId==firmId).Select(e=>e.Email).FirstOrDefault();

            return result;
        }
    }
}
