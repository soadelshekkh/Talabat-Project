using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identities;

namespace Talabat.Api.Extensions
{
    public static class UserMangerExtention
    {
        public static async Task<AppUser> GetUserWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = userManager.Users.Include(A => A.Address).Where(E => E.Email == Email).SingleOrDefault();
            return user;
        }
    }
}
