using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;

namespace ELS.Service
{
    public class AppUserService:IAppUserService
    {
        private ElsDbContext _dbContext;
        public AppUserService(ElsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<AppUserViewModel> GetAppUserAsync(string appUserId)
        {
            throw new NotImplementedException();
        }

        public string UserEmail(string firmId)
        {
            string? result = _dbContext.AppUsers.Where(x=>x.FirmId==firmId).Select(e=>e.Email).FirstOrDefault();

            return result;
        }
    }
}
