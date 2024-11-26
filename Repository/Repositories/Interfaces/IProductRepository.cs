using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task AddImagesAsync(int productId, List<string> imageUrls);
    }
}
