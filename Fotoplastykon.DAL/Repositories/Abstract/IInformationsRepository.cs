using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IInformationsRepository : IRepository<Information>
    {
        Information GetWithCreator(long id);
    }
}
