using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using Talabat.Core.Entities.Identities;

namespace Talabat.Api.DTOS
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
