using Domain.Common;
using System.Drawing.Printing;

namespace Domain.Entities
{
    public class ProductImages : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Image { get; set; }
    }
}
