using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace StockTrackingAutomation.AdminUI.Controllers
{
    public class UnitController : Controller
    {
        private readonly UnitManager _unitManager;
        private readonly IRepository _repository;
        public UnitController(UnitManager unitManager, IRepository repository)
        {
            _unitManager = unitManager;
            _repository = repository;
        }
        public IActionResult Unit()
        {
            var categories = _repository.List<Unit>().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _unitManager.Create(unit);
                return RedirectToAction("Unit", "Unit");
            }
            return View(unit);

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var unit = _repository.List<Unit>(u => u.Id == id).FirstOrDefault();
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }
        [HttpPost]
        public IActionResult Update(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(unit);
                return RedirectToAction(nameof(Unit));
            }
            return View(unit);
        }
        public IActionResult Delete(int id)
        {
            var unitToDelete = _repository.List<Unit>(c => c.Id == id).FirstOrDefault();
            if (unitToDelete != null)
            {
                _repository.Delete(unitToDelete);
                return RedirectToAction("Unit", "Unit"); // Silme işlemi başarılıysa ana sayfaya yönlendir
            }

            return NotFound(); // Kategori bulunamazsa 404 hatası döndür
        }
        public IActionResult Update()
        {
            return View();
        }



    }

}