using AutoMapper;
using Domain.Entities;
using Service.Helpers.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Service.Helpers.DTOs.Categories;
using Service.Helpers.DTOs.Colors;
using Service.Helpers.DTOs.Discounts;
using System.Linq;

namespace Service.Helpers.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Categories
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            #endregion

            #region Products
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(img => img.Image).ToList()))
                .ForMember(dest=>dest.ColorNames,opt=>opt.MapFrom(src=>src.ProductColors.Select(x=>x.Color.Name)))
                .ForMember(dest=>dest.Discounts,opt=>opt.MapFrom(src=>src.ProductDiscounts.Select(x=>x.Discount.Percent)))
                ;

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(file => new ProductImages
                {
                    Image = file.FileName
                }).ToList()))
                .ForMember(dest => dest.ProductColors, opt => opt.MapFrom(src =>
                    src.ColorIds.Select(colorId => new ProductColor
                    {
                        ColorId = colorId
                    })))
                .ForMember(dest => dest.ProductDiscounts, opt => opt.MapFrom(src =>
                    src.DiscountIds.Select(discountId => new ProductDiscount
                    {
                        DiscountId = discountId
                    })));
            ;

            CreateMap<ProductEditDto, Product>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.NewImages.Select(file => new ProductImages
                {
                    Image = file.FileName
                }).ToList()))
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            #endregion

            #region Colors
            CreateMap<Color, ColorDto>()
                .ForMember(dest => dest.ProductNames, opt => opt.MapFrom(src => src.ProductColors.Select(x => x.Product.Name)));
            CreateMap<ColorCreateDto, Color>();
            CreateMap<ColorEditDto, Color>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            #endregion

            #region Discount
            CreateMap<Discount, DiscountDto>()
                .ForMember(dest => dest.ProductNames, opt => opt.MapFrom(src => src.ProductDiscounts.Select(x => x.Product.Name)));
            CreateMap<DiscountCreateDto, Discount>();
            CreateMap<DiscountEditDto, Discount>()
                .ForAllMembers(opts =>
                {
                    opts.AllowNull();
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
            #endregion
        }

        

    }
}
