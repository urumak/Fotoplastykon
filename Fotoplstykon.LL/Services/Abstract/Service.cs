using AutoMapper;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public abstract class Service
    {
        public Service(IUnitOfWork unit, IMapper mapper)
        {
            Unit = unit;
            Mapper = mapper;
        }

        protected IUnitOfWork Unit { get; }
        protected IMapper Mapper { get; }
    }
}
