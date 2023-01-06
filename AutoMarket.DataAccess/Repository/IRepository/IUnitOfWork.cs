using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBrandRepository Brand { get; }
        IFuelTypeRepository FuelType { get; }
        ICarRepository Car { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        void Save();
    }
}
