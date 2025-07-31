using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Conrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace StockTrackingAutomation.AdminUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly IRepository _repository;
        public HomeController(ProductManager productManager, IRepository repository)
        {
            _productManager = productManager;
            _repository = repository;
        }
        public IActionResult Index()
        {
            var products = _repository.GetAll<Product>()
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Unit)
            .ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_repository.GetAll<Category>(), "Id", "CategoryName");
            ViewBag.Brands = new SelectList(_repository.GetAll<Brand>(), "Id", "BrandName");
            ViewBag.Units = new SelectList(_repository.GetAll<Unit>(), "Id", "UnitName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                _repository.Insert(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_repository.GetAll<Category>(), "Id", "CategoryName", product.CategoryId);
            ViewBag.Brands = new SelectList(_repository.GetAll<Brand>(), "Id", "BrandName", product.BrandId);
            ViewBag.Units = new SelectList(_repository.GetAll<Unit>(), "Id", "UnitName", product.UnitId);
            return View(product);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _repository.GetById<Product>(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_repository.GetAll<Category>(), "Id", "CategoryName", product.CategoryId);
            ViewBag.Brands = new SelectList(_repository.GetAll<Brand>(), "Id", "BrandName", product.BrandId);
            ViewBag.Units = new SelectList(_repository.GetAll<Unit>(), "Id", "UnitName", product.UnitId);
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                _repository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_repository.GetAll<Category>(), "Id", "CategoryName", product.CategoryId);
            ViewBag.Brands = new SelectList(_repository.GetAll<Brand>(), "Id", "BrandName", product.BrandId);
            ViewBag.Units = new SelectList(_repository.GetAll<Unit>(), "Id", "UnitName", product.UnitId);
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            var productToDelete = _repository.List<Product>(p => p.Id == id).FirstOrDefault();
            if (productToDelete != null)
            {
                _repository.Delete(productToDelete);
                return RedirectToAction(nameof(Index)); // Silme iþlemi baþarýlýysa ana sayfaya yönlendir
            }

            return NotFound(); // Kategori bulunamazsa 404 hatasý döndür
        }

    }

}
