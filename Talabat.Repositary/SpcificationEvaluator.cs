using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.spcifications;

namespace Talabat.Repositary
{
    public class SpcificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuary(IQueryable<T> quary, IGenericSpecificarion<T> specificarion)
        {
            var MyQuary = quary;
            if (specificarion.Condition != null)
                MyQuary= MyQuary.Where(specificarion.Condition);
            if (specificarion.OrderBy != null)
                MyQuary = MyQuary.OrderBy(specificarion.OrderBy);
            if (specificarion.OrderByDesc != null)
                MyQuary = MyQuary.OrderByDescending(specificarion.OrderByDesc);
            if (specificarion.IsPagination)
                MyQuary = MyQuary.Skip(specificarion.skip).Take(specificarion.Take); 
           
            MyQuary = specificarion.Includes.Aggregate(MyQuary, (CurrentQuary, ContactPart) => CurrentQuary.Include(ContactPart));
            return MyQuary;
        }
    }
}
