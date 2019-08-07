using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.Repositories.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;

namespace Fotoplastykon.DAL.UnitsOfWork.Concrete
{
    public class UsersWithPermissionsUnit : UnitOfWork, IUsersWithPermissionsUnit
    {
        public UsersWithPermissionsUnit(DatabaseContext context)
            : base(context)
        {
            Users = new UserRepository(context);
            Creations = new PageCreationRepository(context);
        }

        public IUserRepository Users { get; }
        public IPageCreationRepository Creations { get; }
    }
}
