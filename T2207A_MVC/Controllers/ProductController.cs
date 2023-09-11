using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using T2207A_MVC.Entities;
using T2207A_MVC.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2207A_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
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
                _context.Products.Add(new Product
                {
                    name = model.name,
                    price = model.price,
                    description = model.description,
                    category_id = model.category_id,
                    brand_id = model.brand_id
                });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

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
                _context.Products.Update(new Product
                {
                    id = model.id,
                    name = model.name,
                    price = model.price,
                    description = model.description,
                    category_id = model.category_id,
                    brand_id = model.brand_id
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

