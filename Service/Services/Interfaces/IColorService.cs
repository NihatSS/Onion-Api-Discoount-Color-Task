using Domain.Entities;
using Service.Helpers.DTOs.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<ColorDto>> GetAllAsync();
        Task<ColorDto> GetByIdAsync(int id);
        Task CreateAsync(ColorCreateDto color);
        Task EditAsync(int id,ColorEditDto color);
        Task DeleteAsync(int id);
        Task<IEnumerable<ColorDto>> SearchByNameAsync(string name);
    }
}
