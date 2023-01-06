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
    public class FuelTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //Bierzemy obiekt dzięki DI
        public FuelTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<FuelType> objFuelTypeList = _unitOfWork.FuelType.GetAll(); //Daje wszystkie marki z tabeli
            return View(objFuelTypeList);
        }

        //Create GET
        public IActionResult Create()
        {
            return View();
        }

        //Create POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Dla bezpieczeństwa, dodaje klucz do posta w formie i go sprawdza tutaj
        public IActionResult Create(FuelType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FuelType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Fuel Type added successfully";
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

            var fuelTypeFromDbFirst = _unitOfWork.FuelType.GetFirstOrDefault(u => u.Id == id);

            if (fuelTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(fuelTypeFromDbFirst);
        }

        //Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Dla bezpieczeństwa, dodaje klucz do posta w formie i go sprawdza tutaj
        public IActionResult Edit(FuelType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.FuelType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Fuel Type updated successfully";
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

            var fuelTypeFromDbFirst = _unitOfWork.FuelType.GetFirstOrDefault(u => u.Id == id);


            if (fuelTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(fuelTypeFromDbFirst);
        }

        //Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] //Dla bezpieczeństwa, dodaje klucz do posta w formie i go sprawdza tutaj
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.FuelType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.FuelType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Fuel Type deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
