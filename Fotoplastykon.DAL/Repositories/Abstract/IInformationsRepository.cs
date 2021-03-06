﻿using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IInformationsRepository : IRepository<Information>
    {
        Task<Information> GetWithCreator(long id);
        Task<IEnumerable<Information>> GetForMainPage(int limit = 5);
    }
}
