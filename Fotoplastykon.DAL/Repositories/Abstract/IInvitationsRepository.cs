﻿using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IInvitationsRepository : IRepository<Invitation>
    {
        Invitation Get(long firstId, long secondId);
    }
}