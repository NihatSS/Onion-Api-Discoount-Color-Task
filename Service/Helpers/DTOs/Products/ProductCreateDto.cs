using Microsoft.AspNetCore.Http;

namespace Service.Helpers.DTOs.Products
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public ICollection<IFormFile> Images { get; set; }
        public ICollection<int> DiscountIds { get; set; }
        public ICollection<int> ColorIds { get; set; }


    }
}
