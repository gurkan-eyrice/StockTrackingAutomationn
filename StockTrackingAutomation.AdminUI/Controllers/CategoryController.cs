using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockTrackingAutomation.AdminUI.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly CategoryManager _categoryManager;
        private readonly IRepository _repository;
        public CategoryController(CategoryManager categoryManager, IRepository repository)
        {
            _categoryManager = categoryManager;
            _repository = repository;
        }
        public IActionResult Category()
        {
            var categories = _repository.List<Category>().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryManager.Create(category);
                return RedirectToAction("Category", "Category");
            }
            return View(category);

        }
        
        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _repository.List<Category>(c => c.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(category);
                return RedirectToAction(nameof(Category));
            }
            return View(category);
        }



        public IActionResult Delete(int id)
        {
            var categoryToDelete = _repository.List<Category>(c => c.Id == id).FirstOrDefault();
            if (categoryToDelete != null)
            {
                _repository.Delete(categoryToDelete);
                return RedirectToAction("Category", "Category"); // Silme işlemi başarılıysa ana sayfaya yönlendir
            }

            return NotFound(); // Kategori bulunamazsa 404 hatası döndür
        }
        public IActionResult Update()
        {
            return View();
        }
        


    }

}

