using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.Repositories.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fotoplastykon.DAL.UnitsOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DatabaseContext context, 
            IUserRepository users, 
            IFilmPageCreationRepository filmPagesCreations, 
            IInvitationRepository invitations, 
            IFriendshipRepository friendships,
            IPersonPageCreationRepository personPagesCreations,
            IInformationRepository informations)
        {
            Context = context;
            Users = users;
            FilmPagesCreations = filmPagesCreations;
            Friendships = friendships;
            Invitations = invitations;
            PersonPagesCreations = personPagesCreations;
            Informations = informations;
        }

        public IUserRepository Users { get; }
        public IFilmPageCreationRepository FilmPagesCreations { get; }
        public IInvitationRepository Invitations { get; }
        public IFriendshipRepository Friendships { get; }
        public IPersonPageCreationRepository PersonPagesCreations { get; }

        public IInformationRepository Informations { get; }

        private DbContext Context { get; }

        public int Complete()
        {
            return Context.SaveChanges();
        }
    }
}
