using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class InvitationsRepository : Repository<Invitation>, IInvitationsRepository
    {
        public InvitationsRepository(DatabaseContext context)
            : base(context)
        {
        }
    }
}
