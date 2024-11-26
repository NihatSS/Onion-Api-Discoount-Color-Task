using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Service.Helpers.DTOs.Products
{
    public class ProductEditDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
        public ICollection<IFormFile> NewImages { get; set; }
    }
}
