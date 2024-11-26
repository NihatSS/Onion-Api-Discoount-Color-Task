using Service.Helpers.DTOs.Colors;
using Service.Helpers.DTOs.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountDto>> GetAllAsync();
        Task<DiscountDto> GetByIdAsync(int id);
        Task CreateAsync(DiscountCreateDto color);
        Task EditAsync(int id, DiscountEditDto color);
        Task DeleteAsync(int id);
        Task<IEnumerable<DiscountDto>> SearchByNameAsync(string name);
    }
}
