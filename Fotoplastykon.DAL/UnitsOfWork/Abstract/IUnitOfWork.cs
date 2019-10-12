using Fotoplastykon.DAL.Repositories.Abstract;
using System;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IFilmPageCreationRepository FilmPagesCreations { get; }
        IInvitationRepository Invitations { get; }
        IFriendshipRepository Friendships { get; }
        IPersonPageCreationRepository PersonPagesCreations { get; }
        IInformationRepository Informations { get; }
        int Complete();
    }
}
