using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.Repositories.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Fotoplastykon.DAL.UnitsOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DatabaseContext context, IUserRepository users, IPageCreationRepository creations)
        {
            Context = context;
            Users = users;
            Creations = creations;
        }

        public IUserRepository Users { get; }
        public IPageCreationRepository Creations { get; }
        private DbContext Context { get; }

        public int Complete()
        {
            return Context.SaveChanges();
        }
    }
}
