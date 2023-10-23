using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.DTOS
{
    public class OrdereItemDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
