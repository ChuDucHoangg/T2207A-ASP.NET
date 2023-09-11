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
    public class BrandController : Controller
    {
        private readonly DataContext _context;

        private readonly IWebHostEnvironment webHostEnvironment;

        public BrandController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var brands = _context.Brands.ToList();
            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        private string UploadLogo(IFormFile logoFile)
        {
            if (logoFile != null && logoFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + logoFile.FileName;
                string logoPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(logoPath, FileMode.Create))
                {
                    logoFile.CopyTo(fileStream);
                }

                return uniqueFileName;
            }
            return null;
        }

        [HttpPost]
        public IActionResult Create(BrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                string logoPath = UploadLogo(model.LogoFile);
                _context.Brands.Add(new Brand { name = model.name, logo = logoPath });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Brand brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(new BrandEditModel { id = brand.id, name = brand.name });
        }

        [HttpPost]
        public IActionResult Edit(BrandEditModel model)
        {
            if (ModelState.IsValid)
            {
                string logoPath = UploadLogo(model.LogoFile);
                _context.Brands.Update(new Brand { id = model.id, name = model.name, logo = logoPath });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Brand brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

