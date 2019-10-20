using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class InvitationsRepository : Repository<Invitation>, IInvitationsRepository
    {
        public InvitationsRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public async Task<Invitation> Get(long firstId, long secondId)
        {
            var invitation = await DatabaseContext.Invitations
                .FirstOrDefaultAsync(f => f.InvitedId == firstId && f.InvitingId == secondId);

            if (invitation == null) invitation = await DatabaseContext.Invitations
                    .FirstOrDefaultAsync(f => f.InvitedId == secondId && f.InvitingId == firstId);

            return invitation;
        }
    }
}
