using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using T2207A_MVC.Entities;
using T2207A_MVC.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2207A_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var categories = _context.Categories.ToList();
            var brands = _context.Brands.ToList();
            ViewBag.category = new SelectList(categories, "id", "name");
            ViewBag.brand = new SelectList(brands, "id", "name");
            return View(products);
        }

        public IActionResult Create()
        {
            var categories = _context.Categories.ToList();
            var brands = _context.Brands.ToList();
            ViewBag.category_id = new SelectList(categories, "id", "name");
            ViewBag.brand_id = new SelectList(brands, "id", "name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            var categories = _context.Categories.ToList();
            var brands = _context.Brands.ToList();
            ViewBag.category_id = new SelectList(categories, "id", "name");
            ViewBag.brand_id = new SelectList(brands, "id", "name");
            if (ModelState.IsValid)
            {
                string imagePath = UploadImage(model.ImageFile);
                _context.Products.Add(new Product
                {
                    name = model.name,
                    price = model.price,
                    description = model.description,
                    category_id = model.category_id,
                    brand_id = model.brand_id,
                    image = imagePath 
                });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

        }

        private string UploadImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string imagePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                return uniqueFileName;
            }
            return null;
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var categories = _context.Categories.ToList();
            var brands = _context.Brands.ToList();
            ViewBag.category_id = new SelectList(categories, "id", "name");
            ViewBag.brand_id = new SelectList(brands, "id", "name");

            return View(new ProductEditModel
            {
                id = product.id, 
                name = product.name,
                price = product.price,
                description = product.description,
                category_id = product.category_id,
                brand_id = product.brand_id
            });
        }

        [HttpPost]
        public IActionResult Edit(ProductEditModel model)
        {
            var categories = _context.Categories.ToList();
            var brands = _context.Brands.ToList();
            ViewBag.category_id = new SelectList(categories, "id", "name");
            ViewBag.brand_id = new SelectList(brands, "id", "name");
            if (ModelState.IsValid)
            {
                string imagePath = UploadImage(model.ImageFile);
                _context.Products.Update(new Product
                {
                    id = model.id,
                    name = model.name,
                    price = model.price,
                    description = model.description,
                    category_id = model.category_id,
                    brand_id = model.brand_id,
                    image = imagePath
                });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

