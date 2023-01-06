using AutoMarket.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Brand = new BrandRepository(_db);
            FuelType = new FuelTypeRepository(_db);
            Car = new CarRepository(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
        }

        public IBrandRepository Brand {get; private set;}
        public IFuelTypeRepository FuelType {get; private set;}
        public ICarRepository Car {get; private set;}
        public ICompanyRepository Company {get; private set;}
        public IShoppingCartRepository ShoppingCart {get; private set;}
        public IApplicationUserRepository ApplicationUser {get; private set;}
        public IOrderDetailRepository OrderDetail {get; private set;}
        public IOrderHeaderRepository OrderHeader {get; private set;}

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
