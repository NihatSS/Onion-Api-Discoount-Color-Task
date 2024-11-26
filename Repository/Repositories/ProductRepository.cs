using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Exceptions;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    internal class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddImagesAsync(int productId, List<string> imageUrls)
        {
            var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId)
                         ?? throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            foreach (var url in imageUrls)
            {
                product.Images.Add(new ProductImages { ProductId = productId, Image = url });
            }

            await _context.SaveChangesAsync();
        }
    }
}
