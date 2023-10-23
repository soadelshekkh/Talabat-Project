using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Reposatiries;
using Talabat.Core.spcifications;
using Talabat.Repositary.DbContexts;

namespace Talabat.Repositary
{
    public class GenericRepositary<T> : IGenericRepositary<T> where T : BaseEntity
    {
        private readonly TalabatContext context;
        public GenericRepositary(TalabatContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
           return await context.Set<T>().ToListAsync();
        }

        public async Task<T>  GetById(int id)
        {
            //return await context.Set<T>().Where(Item => Item.Id == id).FirstOrDefaultAsync();
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdSpec(IGenericSpecificarion<T> spec)
        {
            return await BuildQuary(spec).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllSpec(IGenericSpecificarion<T> spec)
        {
            return await BuildQuary(spec).ToListAsync();
        }
        private IQueryable<T> BuildQuary(IGenericSpecificarion<T> spec)
        {
            return  SpcificationEvaluator<T>.GetQuary(context.Set<T>(), spec);
        }

        public async Task<int> GetCount(IGenericSpecificarion<T> spec)
        {
            return await BuildQuary(spec).CountAsync();
        }

        public async Task Create(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public  void update(T entity)
        {
             context.Set<T>().Update(entity);
        }

        public void delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
