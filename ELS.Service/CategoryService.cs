using ELS.Data;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace ELS.Service
{
    public class CategoryService : ICategoryService
    {
        private ElsDbContext _dbContext;

        public CategoryService(ElsDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            List<CategoryViewModel> categories = await _dbContext.Categories.Select(c=>new CategoryViewModel() 
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
            return categories;
        }
    }
}
