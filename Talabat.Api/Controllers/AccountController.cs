using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Talabat.Api.DTOS;
using Talabat.Api.Errors;
using Talabat.Api.Extensions;
using Talabat.Core.Entities.Identities;
using Talabat.Core.services;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginData)
        {
            var User = await userManager.FindByEmailAsync(loginData.Email);
            if (User == null) return Unauthorized(new ApiResponse(401));
            var CheckPassword = await signInManager.CheckPasswordSignInAsync(User, loginData.Password, false);
            if(!CheckPassword.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = loginData.Email,
                Token = await tokenService.CreateToken(User,userManager)
            });
            
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerData)
        {
            if (CheckEmailExist(registerData.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "this mail already exist" } });
            var User = new AppUser() {
                UserName = registerData.Email.Split("@")[0],
                Email = registerData.Email,
                PhoneNumber = registerData.PhoneNumber,
                DisplayName = registerData.DisplayName
            };
            var result = await userManager.CreateAsync(User, registerData.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await tokenService.CreateToken(User, userManager)
            });

        }
        [Authorize]
        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(Email);
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "helloo"
            });
        }
        [HttpPut("address")]
        public async Task<ActionResult<AdrressDTO>> UpdateAddress (AdrressDTO UpdatedAddress)
        {
            var address = mapper.Map<AdrressDTO, Address>(UpdatedAddress);
            
            var user = await userManager.GetUserWithAddress(User);
            user.Address = address;
            var result =await userManager.UpdateAsync(user);
            if(result.Succeeded)
            return Ok(UpdatedAddress);
            return BadRequest(new ApiResponse(400));
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AdrressDTO>> GetUserWthAddress()
        {
            var user = await userManager.GetUserWithAddress(User);
            var MappedAddress = mapper.Map<Address, AdrressDTO >(user.Address);
            return Ok(MappedAddress);
        }
        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string Email)
        {
            var result = await userManager.FindByEmailAsync(Email);
            if(result == null)
                return Ok(false);
            return Ok(true);
        }
    }
}
