using Fotoplastykon.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.UnitsOfWork
{
    interface IUnitOfWork : IDisposable
    {
        ICoreUserRepository CoreUsers { get; }
        int Save();
    }
}
