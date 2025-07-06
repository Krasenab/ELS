using ELS.Service;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ELS.Controllers
{
    public class EquipmentController : Controller
    {
        private ICategoryService _categoryService;
        private IEquipmentService _equipmentService;
        public EquipmentController(ICategoryService categoryService, IEquipmentService equipmentService)
        {
            _categoryService = categoryService;
            _equipmentService = equipmentService;

        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<CategoryViewModel> allCategories = await _categoryService.GetAllCategoriesAsync();

            AddEquipmentViewModel inputView = new AddEquipmentViewModel() 
            {
               Categories = allCategories
            };
            return View(inputView);
        }


    }
}
