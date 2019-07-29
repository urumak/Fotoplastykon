using Fotoplastykon.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICoreUserRepository CoreUsers { get; }
        int Save();
    }
}
