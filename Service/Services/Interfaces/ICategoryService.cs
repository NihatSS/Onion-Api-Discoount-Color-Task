using Domain.Entities;
using Service.Helpers.DTOs.Categories;

namespace Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateDto category);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> SearchAsync(string str);
        Task EditAsync(int id, CategoryEditDto category);
        Task DeleteAsync(int id);

    }
}
