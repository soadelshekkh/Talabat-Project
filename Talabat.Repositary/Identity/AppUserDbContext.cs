using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identities;

namespace Talabat.Repositary.Identity
{
    public class AppUserDbContext : IdentityDbContext<AppUser>
    {
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options):base (options)
        {

        }
        //public DbSet<AppUser> MyProperty { get; set; }
    }
}
