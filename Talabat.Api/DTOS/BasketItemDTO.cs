using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.DTOS
{
    public class BasketItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Quantity must be more than one")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Product must have price")]
        public decimal price { get; set; }
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
