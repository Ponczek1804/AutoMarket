using AutoMarket.DataAccess.Repository.IRepository;
using AutoMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.DataAccess.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private ApplicationDbContext _db;
        public CarRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Car obj)
        {
            var objFromDb = _db.Cars.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.DiscountPrice = obj.DiscountPrice;
                objFromDb.YearOfProduction = obj.YearOfProduction;
                objFromDb.Mileage = obj.Mileage;
                objFromDb.BrandId = obj.BrandId;
                objFromDb.FuelTypeId = obj.FuelTypeId;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
