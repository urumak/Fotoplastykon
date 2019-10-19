using AutoMapper;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class FilmsService : Service, IFilmsService
    {
        public FilmsService(IUnitOfWork unit, IMapper mapper)
            :base(unit, mapper)
        {

        }

        public void Rate(long userId, long filmId)
        {
        }
    }
}
