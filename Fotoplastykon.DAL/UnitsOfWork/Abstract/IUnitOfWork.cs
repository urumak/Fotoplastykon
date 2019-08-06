using System;
using Fotoplastykon.DAL.Repositories.Abstract;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ICoreUserRepository Users { get; }
        int Complete();
    }
}
