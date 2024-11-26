using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Categories;
using Service.Helpers.DTOs.Products;
using Service.Services.Interfaces;
using System.Data;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepo;

        public ProductService(IProductRepository repo,
                              IMapper mapper,
                              IDiscountRepository discountRepo)
        {
            _mapper = mapper;
            _repo = repo;
            _discountRepo = discountRepo;
        }

        public async Task CreateAsync(ProductCreateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _repo.CreateAsync(product);

            if (productDto.Images != null && productDto.Images.Any())
            {
                var imageUrls = await SaveImagesAsync(productDto.Images);
                await _repo.AddImagesAsync(product.Id, imageUrls);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task EditAsync(int id, ProductEditDto productDto)
        {
            var product = await _repo.GetByIdAsync(id);

            _mapper.Map(productDto, product);
            await _repo.EditAsync(product);

            if (productDto.NewImages != null && productDto.NewImages.Any())
            {
                var imageUrls = await SaveImagesAsync(productDto.NewImages);
                await _repo.AddImagesAsync(product.Id, imageUrls);
            }
        }

        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            var datas = await _repo.GetAllAsync(x => x.Category, x => x.ProductDiscounts, x=>x.ProductColors);


            var mappedDatas = _mapper.Map<ICollection<ProductDto>>(datas);

            foreach (var data in datas)
            {
                var price = data.Price;

                foreach (var item in data.ProductDiscounts)
                {
                    var discount = await _discountRepo.GetByIdAsync(item.DiscountId);
                    price -= (price * discount.Percent / 100);
                }

                var productDto = mappedDatas.FirstOrDefault(x => x.Id == data.Id);
                if (productDto != null)
                {
                    productDto.SalePrice = price;
                }
            }

            return mappedDatas;
        }


        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return _mapper.Map<ProductDto>(await _repo.GetByIdAsync(id, x => x.Category));
        }

        public async Task<IEnumerable<ProductDto>> Search(string str)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _repo.GetAllWithExpression(x => x.Name.ToLower().Trim()
            .Contains(str.ToLower().Trim())));
        }
        private async Task<List<string>> SaveImagesAsync(ICollection<IFormFile> images)
        {
            var imageUrls = new List<string>();
            var imageDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Images");

            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }

            foreach (var image in images)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
                var filePath = Path.Combine(imageDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                imageUrls.Add(fileName);
            }

            return imageUrls;
        }
    }
}
