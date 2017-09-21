using System.Collections.Generic;
using System.Linq;
using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BethanysPieShop.Controllers
{
    [Authorize(Roles = "Administrators")]
    [Authorize(Policy = "DeletePie")]
    public class PieManagementController: Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieManagementController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult Index()
        {
            var pies = _pieRepository.Pies.OrderBy(p => p.PieId);
            return View(pies);
        }

        public IActionResult AddPie()
        {
            var categories = _categoryRepository.Categories;
            var pieEditViewModel = new PieEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() }).ToList(),
                CategoryId = categories.FirstOrDefault().CategoryId
            };
            return View(pieEditViewModel);
        }

        [HttpPost]
        public IActionResult AddPie(PieEditViewModel pieEditViewModel)
        {
            //Basic validation
            //if (ModelState.IsValid)
            //{
            //    _pieRepository.CreatePie(pieEditViewModel.Pie);
            //    return RedirectToAction("Index");
            //}

            //custom validation rules
            if (ModelState.GetValidationState("Pie.Price") == ModelValidationState.Valid
                || pieEditViewModel.Pie.Price < 0)
                ModelState.AddModelError(nameof(pieEditViewModel.Pie.Price), "The price of the pie should be higher than 0");

            if (pieEditViewModel.Pie.IsPieOfTheWeek && !pieEditViewModel.Pie.InStock)
                ModelState.AddModelError(nameof(pieEditViewModel.Pie.IsPieOfTheWeek), "Only pies that are in stock should be Pie of the Week");

            if (ModelState.IsValid)
            {
                _pieRepository.CreatePie(pieEditViewModel.Pie);
                return RedirectToAction("Index");
            }

            return View(pieEditViewModel);
        }

        //public IActionResult EditPie([FromRoute]int pieId)
        //public IActionResult EditPie([FromQuery]int pieId, [FromHeader] string accept)
        public IActionResult EditPie([FromQuery]int pieId, [FromHeader(Name = "Accept-Language")] string accept)
        {
            var categories = _categoryRepository.Categories;

            var pie = _pieRepository.Pies.FirstOrDefault(p => p.PieId == pieId);

            var pieEditViewModel = new PieEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() }).ToList(),
                Pie = pie,
                CategoryId = pie.CategoryId
            };

            var item = pieEditViewModel.Categories.FirstOrDefault(c => c.Value == pie.CategoryId.ToString());
            item.Selected = true;

            return View(pieEditViewModel);
        }

        [HttpPost]
        //public IActionResult EditPie([Bind("Pie")] PieEditViewModel pieEditViewModel)
        public IActionResult EditPie(PieEditViewModel pieEditViewModel)
        {
            pieEditViewModel.Pie.CategoryId = pieEditViewModel.CategoryId;

            if (ModelState.IsValid)
            {
                _pieRepository.UpdatePie(pieEditViewModel.Pie);
                return RedirectToAction("Index");
            }
            return View(pieEditViewModel);
        }

        [HttpPost]
        public IActionResult DeletePie(string pieId)
        {
            return View();
        }

        public IActionResult QuickEdit()
        {
            var pieNames = _pieRepository.Pies.Select(p => p.Name).ToList();
            return View(pieNames);
        }

        [HttpPost]
        public IActionResult QuickEdit(List<string> pieNames)
        {
            //Do awesome things with the pie names here
            return View();
        }

        public IActionResult BulkEditPies()
        {
            var pieNames = _pieRepository.Pies.ToList();
            return View(pieNames);
        }

        [HttpPost]
        public IActionResult BulkEditPies(List<Pie> pies)
        {
            //Do awesome things with the pie here
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckIfPieNameAlreadyExists([Bind(Prefix = "Pie.Name")]string name)
        {
            var pie = _pieRepository.Pies.FirstOrDefault(p => p.Name == name);
            return pie == null ? Json(true) : Json("That pie name is already taken");
        }
    }
}