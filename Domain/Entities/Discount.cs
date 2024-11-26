using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Discount : BaseEntity
    {
        public string Name { get; set; }
        public int Percent { get; set; }
        public ICollection<ProductDiscount> ProductDiscounts { get; set; }

    }
}
