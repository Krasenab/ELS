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
               EquipmentStatuses = _equipmentService.GetEuqipmentStatues()               
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

            FilteredEquipmentViewModel model = new FilteredEquipmentViewModel()
            {
                SearchTerm = "",
                StatusFilter = "",
                AllEquipment = await _equipmentService.GetAllEquipmentAsync(),
                Categories = await _categoryService.GetAllCategoriesAsync()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllFiltered(string searchTerm, string category) 
        {
          var view = await _equipmentService.GetAllFilteredEquipmentAsync(searchTerm, category);
            return PartialView("_AllFilteredEquipmentPartial", view);
        } 
        


        [HttpGet]
        public async Task<IActionResult> Edit(string id) 
        {
            EditEquipmentViewModel e = await _equipmentService.GetEquipmetForEditByIdAsync(id);
            if (e == null) 
            {

            }
            e.EquipmentStatuses = _equipmentService.GetEuqipmentStatues();
            e.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(e);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditEquipmentViewModel view) 
        {
             
            await _equipmentService.EditEquipmentAsync(view);   
            return RedirectToAction("All", "Equipment");
        }


    }
}
