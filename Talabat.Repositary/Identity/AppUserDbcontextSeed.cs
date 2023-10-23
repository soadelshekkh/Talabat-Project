using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identities;

namespace Talabat.Repositary.Identity
{
    public class AppUserDbcontextSeed
    {
        public static async Task CreateAppUser(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any()) { 
                var User = new AppUser()
                {
                    UserName = "SoadElshekh",
                    Email = "SoadElshekh5000@Gmail.com",
                    DisplayName = "Soad AbdElrouf Elshekh"
                };
                await userManager.CreateAsync(User,"Soad#Password1234");
                //"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJTb2FkRWxzaGVraDUwMDBAR21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiU29hZCBBYmRFbHJvdWYgRWxzaGVraCIsImV4cCI6MTY5NTE3NjIxMSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMS8iLCJhdWQiOiJNeVNlY3VyZWRBdWRpZW5jZSJ9.jHrFPSGR1Oy44xtN9klRNyHSEH8vH5ltQfXL9nNDFQk
            }
        }
    }
}
