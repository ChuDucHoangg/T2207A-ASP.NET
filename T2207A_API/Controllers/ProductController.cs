using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T2207A_API.Entities;
using T2207A_API.DTOs;
using T2207A_API.Models.Product;
using T2207A_API.Models.Category;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace T2207A_API.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : Controller
    {

        private readonly T2207aApiContext _context;

        public ProductController(T2207aApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            List<ProductDTO> data = new List<ProductDTO>();
            foreach (Product c in products)
            {
                data.Add(new ProductDTO { id = c.Id, name = c.Name, price = c.Price, description = c.Description, thumbnail = c.Thumbnail, qty = c.Qty, CategoryId = c.CategoryId });
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            try
            {
                Product c = _context.Products.Find(id);
                if (c != null)
                {
                    return Ok(new ProductDTO { id = c.Id, name = c.Name, price = c.Price, description = c.Description, thumbnail = c.Thumbnail, qty = c.Qty, CategoryId = c.CategoryId });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("get-by-category")]
        public IActionResult GetByCategory(int categoryId)
        {
            try
            {
                List<Product> products = _context.Products.Where(p => p.CategoryId == categoryId).ToList();
                List<ProductDTO> data = products.Select(p => new ProductDTO
                {
                    id = p.Id,
                    name = p.Name,
                    price = p.Price,
                    description = p.Description,
                    thumbnail = p.Thumbnail,
                    qty = p.Qty,
                    CategoryId = p.CategoryId
                }).ToList();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public IActionResult Create(CreateProduct model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product data = new Product { Name = model.name, Price = model.price, Description = model.description, Thumbnail = model.thumbnail, Qty = model.qty, CategoryId = model.category };
                    _context.Products.Add(data);
                    _context.SaveChanges();
                    return Created($"get-by-id?id={data.Id}", new ProductDTO { id = data.Id, name = data.Name, price = data.Price, description = data.Description, thumbnail = data.Thumbnail, qty = data.Qty, CategoryId = data.CategoryId });
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
            var msgs = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
            return BadRequest(string.Join(" | ", msgs));
        }

        [HttpPut]
        public IActionResult Update(EditProduct model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = new Product { Id = model.id, Name = model.name, Price = model.price, Qty = model.qty, Description = model.description, Thumbnail = model.thumbnail, CategoryId = model.category };
                    if (product != null)
                    {
                        _context.Products.Update(product);
                        _context.SaveChanges();
                        return NoContent();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);

                }

            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                Product product = _context.Products.Find(id);
                if (product == null)
                    return NotFound();
                _context.Products.Remove(product);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

