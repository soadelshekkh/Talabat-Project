using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.spcifications
{
    public class BaseSpecificaton<T> : IGenericSpecificarion<T> where T : BaseEntity
    {
        public BaseSpecificaton(Expression<Func<T, bool>> Condition)
        {
            this.Condition = Condition;
        }
        public BaseSpecificaton()
        {

        }
        public Expression<Func<T, bool>> Condition { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; } = false;

        public void GetorderByExperresion(Expression<Func<T,object>> expression)
        {
            OrderBy = expression;   
        }
        public void GetorderByExperresiondesc(Expression<Func<T, object>> expression)
        {
            OrderByDesc = expression;
        }
        public void ApplyPagination(int skip, int Take)
        {
            this.skip = skip;
            this.Take = Take;
            this.IsPagination = true;
        }

       
    }
}
