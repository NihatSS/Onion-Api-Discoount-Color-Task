using Domain.Common;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductImages> Images { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductDiscount> ProductDiscounts { get; set; }

    }
}
