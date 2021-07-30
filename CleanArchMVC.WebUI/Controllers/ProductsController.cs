using CleanArchMVC.Application.DTOs;
using CleanArchMVC.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchMVC.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(ICategoryService categoryService, IProductService productService, IWebHostEnvironment environment)
        {
            _categoryService = categoryService;
            _productService = productService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        [HttpGet()]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
                new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                await _productService.Add(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var productDTO = await _productService.GetById(id);

            if (productDTO == null) return NotFound();

            var categories = await _categoryService.GetCategories();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDTO.CategoryId);

            return View(productDTO);
        
        
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if(ModelState.IsValid)
            {
                await _productService.Update(productDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(productDTO);
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var productDTO = await _productService.GetById(id);

            if (productDTO == null) return NotFound();

            return View(productDTO);
            
        }


        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var productDTO = await _productService.GetById(id);

            if (productDTO == null) return NotFound();

            var wwwroot = _environment.WebRootPath;

            var image = Path.Combine(wwwroot, "images\\" + productDTO.Image);

            var exists = System.IO.File.Exists(image);

            ViewBag.ImageExist = exists;

            return View(productDTO);

        }
    }
}
