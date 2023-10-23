using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Reposatiries;
using Talabat.Repositary.DbContexts;

namespace Talabat.Repositary
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalabatContext context;
        private Hashtable Repositaries;
        public UnitOfWork(TalabatContext context)
        {
            this.context = context;
        }
        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepositary<TEntity> Repositary<TEntity>() where TEntity : BaseEntity
        {
            if(Repositaries==null)
                Repositaries = new Hashtable(); 
            var type = typeof(TEntity).Name;
            if(!Repositaries.ContainsKey(type))
            {
                var repositary = new GenericRepositary<TEntity>(context);
                Repositaries.Add(type, repositary);
            }
            return (IGenericRepositary<TEntity>) Repositaries[type];
        }
    }
}
