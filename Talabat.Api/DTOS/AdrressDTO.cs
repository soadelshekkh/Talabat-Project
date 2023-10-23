using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.DTOS
{
    public class AdrressDTO
    {
        public int Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string street { get; set; }
    }
}
