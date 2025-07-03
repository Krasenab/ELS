using ELS.Data;

namespace ELS.Service
{
    public class AppUserService
    {
        private ElsDbContext _dbContext;
        public AppUserService(ElsDbContext dbContext)
        {
            _dbContext = dbContext;
        }




    }
}
