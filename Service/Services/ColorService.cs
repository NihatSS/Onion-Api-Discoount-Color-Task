using AutoMapper;
using Domain.Entities;
using Repository.Exceptions;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Colors;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repo;
        private readonly IMapper _mapper;
        public ColorService(IColorRepository repo,
                            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateAsync(ColorCreateDto color)
        {
            await _repo.CreateAsync(_mapper.Map<Color>(color));
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task EditAsync(int id,ColorEditDto color)
        {
            var existColor = await _repo.GetByIdAsync(id);

            if (existColor == null) throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            _mapper.Map(color, existColor);

            await _repo.EditAsync(existColor);

        }

        public async Task<IEnumerable<ColorDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ColorDto>>(await _repo.GetAllAsync());
        }

        public async Task<ColorDto> GetByIdAsync(int id)
        {
            var color = await _repo.GetByIdAsync(id)
                                        ?? throw new NotFoundException(ExceptionMessages.NotFoundMessage);

            return _mapper.Map<ColorDto>(color);
        }

        public async Task<IEnumerable<ColorDto>> SearchByNameAsync(string name)
        {
            return _mapper.Map<IEnumerable<ColorDto>>(await _repo.GetAllWithExpression(x => x.Name.Trim()
                                                                                                  .ToLower()
                                                                                                  .Contains(name.ToLower()
                                                                                                                .Trim())));
        }
    }
}
