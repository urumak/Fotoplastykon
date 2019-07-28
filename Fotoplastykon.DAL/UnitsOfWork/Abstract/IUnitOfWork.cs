using Fotoplastykon.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.UnitsOfWork
{
    interface IUnitOfWork : IDisposable
    {
        CoreUserRepository CoreUsers { get; }
        int Save();
    }
}
