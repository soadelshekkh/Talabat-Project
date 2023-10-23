using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.spcifications;

namespace Talabat.Core.Reposatiries
{
    public interface IGenericRepositary<T> where  T: BaseEntity  
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAllSpec(IGenericSpecificarion<T> spec);
        Task<T> GetByIdSpec(IGenericSpecificarion<T> spec);
        Task<int> GetCount(IGenericSpecificarion<T> spec);
        Task Create(T entity);
        void update(T entity);
        void delete(T entity);
    }
}
