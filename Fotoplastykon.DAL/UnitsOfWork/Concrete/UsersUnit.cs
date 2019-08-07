using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.Repositories.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;

namespace Fotoplastykon.DAL.UnitsOfWork.Concrete
{
    public class UsersUnit : UnitOfWork ,IUsersUnit
    {
        public IUserRepository Users { get; }

        public UsersUnit(DatabaseContext context)
            :base(context)
        {
            Users = new UserRepository(context);
        }
    }
}
