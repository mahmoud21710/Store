using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Services.Contract;
using Store.G04.Core;
using Store.G04.Ropository;
using Store.G04.Ropository.Data.Contexts;
using Store.G04.Service.Services.Products;
using Store.G04.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Error;
using Store.G04.Service.Services.Basket;
using StackExchange.Redis;
using Store.G04.Core.Mapping.Basket;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Ropository.Repositories;

namespace Store.G04.APIs.Helper
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDependancy(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwagerServices();
            services.AddDbContextServices(configuration);
            services.AddUserDefinedServices();
            services.AddAutoMapperServices(configuration);
            services.ConfigureInvalidModelStateResponseServices();
            services.AddRedisServices(configuration);

            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
        private static IServiceCollection AddSwagerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        private static IServiceCollection AddDbContextServices(this IServiceCollection services ,IConfiguration configuration )
        {
            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services  )
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketServices>();

            return services;
        }
        private static IServiceCollection AddAutoMapperServices(this IServiceCollection services  , IConfiguration configuration )
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfiler()));
            return services;
        }
        private static IServiceCollection ConfigureInvalidModelStateResponseServices(this IServiceCollection services   )
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                            .SelectMany(P => P.Value.Errors)
                                            .Select(E => E.ErrorMessage)
                                            .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {
                        Erros = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });


            return services;
        }
        private static IServiceCollection AddRedisServices(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
            {
                var connect = configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connect);
            });

            return services;
        }
    }
}
