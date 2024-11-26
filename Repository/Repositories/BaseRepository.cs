using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Exceptions;
using Repository.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id)
                                          ?? throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

        }

        public async Task EditAsync(T entity)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _context.Products.Include(x => x.Category)
                                              .Include(x => x.ProductColors)
                                              .ThenInclude(x => x.Color)
                                              .Include(x => x.ProductDiscounts)
                                              .ThenInclude(x => x.Discount)
                                              .ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetWithIncludesAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate)
                   ?? throw new NotFoundException(ExceptionMessages.NotFoundMessage);
        }


        public async Task<IEnumerable<T>> GetAllWithExpression(Expression<Func<T, bool>> predicate)
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _context.Products
                    .Include(x => x.Category).Include(x => x.Images)
                    .Where(predicate as Expression<Func<Product, bool>>)
                    .ToListAsync();
            }
            return await _dbSet.Where(predicate).ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(entity => entity.Id == id)
                   ?? throw new NotFoundException(ExceptionMessages.NotFoundMessage);
        }

        public async Task<T> GetWithExpression(Expression<Func<T, bool>> predicate)
        {
            if (typeof(T) == typeof(Product))
            {
                var product = await _context.Products
                    .Include(x => x.Category).Include(x => x.Images)
                    .FirstOrDefaultAsync(predicate as Expression<Func<Product, bool>>);

                if (product == null)
                    throw new NotFoundException(ExceptionMessages.NotFoundMessage);

                return (T)(object)product;
            }

            var entity = await _dbSet.FirstOrDefaultAsync(predicate);
            return entity ?? throw new NotFoundException(ExceptionMessages.NotFoundMessage);
        }
    }
}
