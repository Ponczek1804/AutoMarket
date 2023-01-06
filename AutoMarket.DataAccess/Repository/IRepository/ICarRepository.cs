using AutoMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.DataAccess.Repository.IRepository
{
    public interface ICarRepository : IRepository<Car>
    {
        void Update(Car obj);
    }
}
