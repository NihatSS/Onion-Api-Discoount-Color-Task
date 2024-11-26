using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Services.Interfaces;
using Service.Services;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicelayer(this IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(v => { v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IDiscountService, DiscountService>();
            return services;
        }

    }
}
