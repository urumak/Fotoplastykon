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
            IUsersRepository users, 
            IFilmPageCreationsRepository filmPagesCreations, 
            IInvitationsRepository invitations, 
            IFriendshipsRepository friendships,
            IPersonPageCreationsRepository personPagesCreations,
            IInformationsRepository informations)
        {
            Context = context;
            Users = users;
            FilmPagesCreations = filmPagesCreations;
            Friendships = friendships;
            Invitations = invitations;
            PersonPagesCreations = personPagesCreations;
            Informations = informations;
        }

        public IUsersRepository Users { get; }
        public IFilmPageCreationsRepository FilmPagesCreations { get; }
        public IInvitationsRepository Invitations { get; }
        public IFriendshipsRepository Friendships { get; }
        public IPersonPageCreationsRepository PersonPagesCreations { get; }

        public IInformationsRepository Informations { get; }

        private DbContext Context { get; }

        public int Complete()
        {
            return Context.SaveChanges();
        }
    }
}
