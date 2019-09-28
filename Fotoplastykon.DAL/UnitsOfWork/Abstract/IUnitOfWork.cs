using Fotoplastykon.DAL.Repositories.Abstract;
using System;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPageCreationRepository Creations { get; }
        IInvitationRepository Invitations { get; }
        IFriendshipRepository Friendships { get; }
        int Complete();
    }
}
