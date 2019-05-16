using Oceanic.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oceanic.Infrastructure.Interfaces
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
    }
}
