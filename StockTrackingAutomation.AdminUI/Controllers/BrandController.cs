using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace StockTrackingAutomation.AdminUI.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandManager _brandManager;
        private readonly IRepository _repository;
        public BrandController(BrandManager brandManager, IRepository repository)
        {
            _brandManager = brandManager;
            _repository = repository;
        }

        public IActionResult Brand()
        {
            var brands = _repository.List<Brand>().ToList();
            return View(brands);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandManager.Create(brand);
                return RedirectToAction("Brand", "Brand");
            }
            return View(brand);

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var brand = _repository.List<Brand>(b => b.Id == id).FirstOrDefault();
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        public IActionResult Update(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(brand);
                return RedirectToAction(nameof(Brand));
            }
            return View(brand);
        }



        public IActionResult Delete(int id)
        {
            var brandToDelete = _repository.List<Brand>(b => b.Id == id).FirstOrDefault();
            if (brandToDelete != null)
            {
                _repository.Delete(brandToDelete);
                return RedirectToAction("Brand", "Brand"); // Silme işlemi başarılıysa ana sayfaya yönlendir
            }

            return NotFound(); // Kategori bulunamazsa 404 hatası döndür
        }
        public IActionResult Update()
        {
            return View();
        }



    }

}
