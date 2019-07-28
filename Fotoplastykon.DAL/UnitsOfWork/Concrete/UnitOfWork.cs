using System;
using System.Collections.Generic;
using System.Text;
using Fotoplastykon.DAL.Repositories;

namespace Fotoplastykon.DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public CoreUserRepository CoreUsers { get; private set; }
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            CoreUsers = new CoreUserRepository(_context);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
