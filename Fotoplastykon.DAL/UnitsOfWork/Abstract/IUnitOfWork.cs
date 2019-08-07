using System;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
