using Service.Helpers.DTOs.Products;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateDto product);
        Task DeleteAsync(int id);
        Task<ICollection<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> Search(string str);
        Task EditAsync(int id,ProductEditDto product);
    }
}
