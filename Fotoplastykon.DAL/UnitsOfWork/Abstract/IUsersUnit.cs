using Fotoplastykon.DAL.Repositories.Abstract;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public interface IUsersUnit : IUnitOfWork
    {
        IUserRepository Users { get; }
    }
}
