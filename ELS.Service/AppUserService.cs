using ELS.Data;
using ELS.Service.Interfaces;

namespace ELS.Service
{
    public class AppUserService:IAppUserService
    {
        private ElsDbContext _dbContext;
        public AppUserService(ElsDbContext dbContext)
        {
            _dbContext = dbContext;
        }




    }
}
