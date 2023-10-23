﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identities;

namespace Talabat.Core.services
{
    public   interface ITokenService
    {
        Task <string> CreateToken(AppUser user, UserManager<AppUser> userManager);
    }
}
