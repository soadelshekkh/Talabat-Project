using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Reposatiries
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositary<TEntity> Repositary<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
