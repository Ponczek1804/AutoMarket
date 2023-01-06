﻿using AutoMarket.DataAccess.Repository.IRepository;
using AutoMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}