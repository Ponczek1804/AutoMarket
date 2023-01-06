using AutoMarket.DataAccess;
using AutoMarket.DataAccess.Repository.IRepository;
using AutoMarket.Models;
using AutoMarket.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarketWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //Bierzemy obiekt dzięki DI
        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Brand> objBrandList = _unitOfWork.Brand.GetAll(); //Daje wszystkie marki z tabeli
            return View(objBrandList);
        }

        //Create GET
        public IActionResult Create()
        {
            return View();
        }

        //Create POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Dla bezpieczeństwa, dodaje klucz do posta w formie i go sprawdza tutaj
        public IActionResult Create(Brand obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Brand.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Brand added successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Edit GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var brandFromDb = _db.Brands.Find(id);
            var brandFromDbFirst = _unitOfWork.Brand.GetFirstOrDefault(u => u.Id == id);
            //var brandFromDbSingle = _db.Brands.SingleOrDefault(u => u.Id == id);

            if (brandFromDbFirst == null)
            {
                return NotFound();
            }

            return View(brandFromDbFirst);
        }

        //Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Dla bezpieczeństwa, dodaje klucz do posta w formie i go sprawdza tutaj
        public IActionResult Edit(Brand obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Brand.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Brand updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Delete GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var brandFromDb = _db.Brands.Find(id);
            var brandFromDbFirst = _unitOfWork.Brand.GetFirstOrDefault(u => u.Id == id);
            //var brandFromDbSingle = _db.Brands.SingleOrDefault(u => u.Id == id);

            if (brandFromDbFirst == null)
            {
                return NotFound();
            }

            return View(brandFromDbFirst);
        }

        //Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Dla bezpieczeństwa, dodaje klucz do posta w formie i go sprawdza tutaj
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Brand.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Brand.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Brand deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
