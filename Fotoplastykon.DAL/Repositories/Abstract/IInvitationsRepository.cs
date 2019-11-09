using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IInvitationsRepository : IRepository<Invitation>
    {
        Task<Invitation> Get(long firstId, long secondId);
        Task<Invitation> GetByInvitationRoles(long invitedId, long invitingId);
    }
}
