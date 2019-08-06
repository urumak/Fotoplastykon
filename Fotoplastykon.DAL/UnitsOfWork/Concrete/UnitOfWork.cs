using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.Repositories.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;

namespace Fotoplastykon.DAL.UnitsOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICoreUserRepository Users { get; private set; }
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
