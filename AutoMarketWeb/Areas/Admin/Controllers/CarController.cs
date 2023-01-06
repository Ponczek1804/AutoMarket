using AutoMarket.DataAccess;
using AutoMarket.DataAccess.Repository.IRepository;
using AutoMarket.Models;
using AutoMarket.Models.ViewModels;
using AutoMarket.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace AutoMarketWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //Bierzemy obiekt dzięki DI
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Upsert GET
        public IActionResult Upsert(int? id)
        {
            CarVM carVM = new()
            {
                Car = new(),
                BrandList = _unitOfWork.Brand.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                FuelTypeList = _unitOfWork.FuelType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //CREATE
                //ViewBag.BrandList = BrandList;
                //ViewBag.FuelTypeList = FuelTypeList;
                return View(carVM);
            }
            else
            {
                //UPDATE
                carVM.Car = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == id);
                return View(carVM);
            }
        }

        //Upsert POST
        [HttpPost]
        [ValidateAntiForgeryToken] //Dla bezpieczeństwa, dodaje klucz do posta w formie i go sprawdza tutaj
        public IActionResult Upsert(CarVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\cars");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.Car.ImageUrl != null) //Delete old image when updt
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Car.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension),FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Car.ImageUrl = @"\images\cars\" + fileName + extension;
                }
                if(obj.Car.Id == 0)
                {
                    _unitOfWork.Car.Add(obj.Car);
                    TempData["success"] = "Car added successfully";
                }
                else
                {
                    _unitOfWork.Car.Update(obj.Car);
                    TempData["success"] = "Car updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var carList = _unitOfWork.Car.GetAll(includeProperties:"Brand,FuelType");
            return Json(new {data=carList});
        }

        //Delete POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Car.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Car.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successfull" });
        }
        #endregion
    }
}
