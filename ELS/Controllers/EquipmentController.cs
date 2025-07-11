using Els.ViewModels.Enums;
using ELS.Service;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
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
               Categories = allCategories,
               EquipmentStatuses = _equipmentService.getEuqipmentStatues()
               
            };
            return View(inputView);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEquipmentViewModel viewModel) 
        {
          await  _equipmentService.AddEquipmentAsync(viewModel);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> All() 
        {
            
           List<AllEquipmentViewModel> getAll = await _equipmentService.GetAllEquipmentAsync();
            return View(getAll);
        }


    }
}
