using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.Repositories.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Fotoplastykon.DAL.UnitsOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DatabaseContext context, 
            IUserRepository users, 
            IPageCreationRepository creations, 
            IInvitationRepository invitations, 
            IFriendshipRepository friendships)
        {
            Context = context;
            Users = users;
            Creations = creations;
            Friendships = friendships;
            Invitations = invitations;
        }

        public IUserRepository Users { get; }
        public IPageCreationRepository Creations { get; }
        public IInvitationRepository Invitations { get; }
        public IFriendshipRepository Friendships { get; }
        private DbContext Context { get; }

        public int Complete()
        {
            return Context.SaveChanges();
        }
    }
}
