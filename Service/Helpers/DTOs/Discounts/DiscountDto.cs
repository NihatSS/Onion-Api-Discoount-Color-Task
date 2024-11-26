using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.Discounts
{
    public class DiscountDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<string> ProductNames { get; set; }
    }
}
