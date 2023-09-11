using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T2207A_MVC.Entities;
using T2207A_MVC.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2207A_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoryController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            //ViewData["categories"] = categories;   //for title,...
            //ViewBag.categories = categories;
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        private string UploadIcon(IFormFile iconFile)
        {
            if (iconFile != null && iconFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + iconFile.FileName;
                string iconPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(iconPath, FileMode.Create))
                {
                    iconFile.CopyTo(fileStream);
                }

                return uniqueFileName;
            }
            return null;
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string iconPath = UploadIcon(model.IconFile);
                _context.Categories.Add(new Category { name = model.name, icon = iconPath });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }

            return View(new CategoryEditModel { id=category.id, name=category.name });
        }

        [HttpPost]
        public IActionResult Edit(CategoryEditModel model)
        {
            if (ModelState.IsValid)
            {
                string iconPath = UploadIcon(model.IconFile);
                _context.Categories.Update(new Category { id = model.id, name = model.name, icon = iconPath });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

