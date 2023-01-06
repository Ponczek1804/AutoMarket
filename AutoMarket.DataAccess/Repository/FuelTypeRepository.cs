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
    public class FuelTypeRepository : Repository<FuelType>, IFuelTypeRepository
    {
        private ApplicationDbContext _db;
        public FuelTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FuelType obj)
        {
            _db.FuelTypes.Update(obj);
        }
    }
}
