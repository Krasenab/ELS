using AspNetCoreGeneratedDocument;
using Els.ViewModels.Enums;
using ELS.Service;
using ELS.Service.Interfaces;
using ELS.ViewModels;
using ElsModels.SQL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace ELS.Controllers
{
    public class EquipmentController : Controller
    {
        private ICategoryService _categoryService;
        private IEquipmentService _equipmentService;
        private ITicketService _ticketService;
        public EquipmentController(ICategoryService categoryService, IEquipmentService equipmentService,ITicketService ticketService)
        {
            _categoryService = categoryService;
            _equipmentService = equipmentService;
            _ticketService = ticketService;

        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<CategoryViewModel> allCategories = await _categoryService.GetAllCategoriesAsync();
            AddEquipmentViewModel inputView = new AddEquipmentViewModel() 
            {
               Categories = allCategories,
               EquipmentStatuses = _equipmentService.GetEuqipmentStatuses()               
            };
            return View(inputView);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEquipmentViewModel viewModel) 
        {
            viewModel.EquipmentStatuses = _equipmentService.GetEuqipmentStatuses();
            viewModel.Categories = await _categoryService.GetAllCategoriesAsync();
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Count > 0)
                    {
                        Console.WriteLine($"Model error: {key} = {state.Errors[0].ErrorMessage}");
                    }
                }
                TempData["ErrorMessage"] = "Invalid form";
                return View(viewModel);
            }

            await  _equipmentService.AddEquipmentAsync(viewModel);
            TempData["SuccessMessage"] = "Successfully added to the inventory";
            
            return RedirectToAction("All", "Equipment");
        }

        [HttpGet]
        public async Task<IActionResult> All() 
        {

            FilteredEquipmentViewModel model = new FilteredEquipmentViewModel()
            {
                SearchTerm = "",
                CategoryFilter = "",
                StatusFilter = "",
                AllEquipment = await _equipmentService.GetAllEquipmentAsync(),
                Categories = await _categoryService.GetAllCategoriesAsync(),
                Statuses = _equipmentService.GetEuqipmentStatuses(),
                
                
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllFiltered(string searchTerm, string category,string status,int page=1) 
        {
            FilteredEquipmentViewModel filteredEquipment = await _equipmentService.GetAllFilteredEquipmentAsync(searchTerm, category, status,page);


            return PartialView("_AllFilteredEquipmentPartial", filteredEquipment);
        } 
        


        [HttpGet]
        public async Task<IActionResult> Edit(string id) 
        {
            EditEquipmentViewModel e = await _equipmentService.GetEquipmetForEditByIdAsync(id);
           
            e.EquipmentStatuses = _equipmentService.GetEuqipmentStatuses();
            e.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(e);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditEquipmentViewModel view) 
        {
             
            await _equipmentService.EditEquipmentAsync(view);   
            return RedirectToAction("All", "Equipment");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id) 
        {

            EquipmentDetailsViewModel  equipment = await _equipmentService.GetEquipmentAsync(id);
            return View(equipment);
        }
        
        public async Task<IActionResult> Delete(string id) 
        {
            await _ticketService.DeleteAllEquipmentTicketsAsync(id);
            await _equipmentService.RemoveEquipmentAsync(id);

            return RedirectToAction("All", "Equipment");
        }

    }
}
