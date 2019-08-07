using Microsoft.EntityFrameworkCore;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private DbContext Context { get; }

        protected UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
