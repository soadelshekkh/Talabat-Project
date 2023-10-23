using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Talabat.Core.Entities.Identities;
using Talabat.Core.services;

namespace Talabat.Services
{
    public class TokenServices : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser user , UserManager<AppUser> userManager)    
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName  ,user.DisplayName)

            };
            var Roles = await userManager.GetRolesAsync(user);
            foreach(var Role in Roles)  
                AuthClaims.Add(new Claim(ClaimTypes.Role, Role));
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( configuration["JWT:Key"]));
            var Token = new JwtSecurityToken( 
            issuer : configuration["JWT:ValidIssuer"],
            audience : configuration["JWT:ValidAudience"],
            expires : DateTime.Now.AddDays( double.Parse( configuration["JWT:DurationInDays"]) ),
            claims : AuthClaims,
            signingCredentials : new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
