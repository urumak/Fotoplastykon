using Fotoplastykon.DAL.Repositories.Abstract;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public interface IUsersWithPermissionsUnit : IUnitOfWork
    {
        IUserRepository Users { get; }
        IPageCreationRepository Creations { get; }
    }
}
