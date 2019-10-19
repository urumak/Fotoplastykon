using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IPersonMarksRepository : IRepository<PersonMark>
    {
        PersonMark Get(long userId, long personId);
    }
}
