using AutoMapper;
using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Categories;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository,
                               IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(CategoryCreateDto category)
        {
            var result = _mapper.Map<Category>(category);
            await _repository.CreateAsync(result);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task EditAsync(int id, CategoryEditDto category)
        {
            var exisCategory = await _repository.GetByIdAsync(id);

            _mapper.Map(exisCategory, category);

            await _repository.EditAsync(exisCategory);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _repository.GetAllAsync());
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
           return _mapper.Map<CategoryDto>(await _repository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<CategoryDto>> SearchAsync(string str)
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _repository.GetAllWithExpression(x => x.Name.ToLower()
                                                                                                           .Trim()
                                                                                                           .Contains(str.ToLower()
                                                                                                                        .Trim())));
        }
    }
}
