
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.G04.APIs.Error;
using Store.G04.APIs.Helper;
using Store.G04.APIs.MiddeelWare;
using Store.G04.Core;
using Store.G04.Core.Mapping.Products;
using Store.G04.Core.Services.Contract;
using Store.G04.Ropository;
using Store.G04.Ropository.Data;
using Store.G04.Ropository.Data.Contexts;
using Store.G04.Service.Services.Products;

namespace Store.G04.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependancy(builder.Configuration);

            var app = builder.Build();

            await app.ConfigreMiddleWaresAsync();

            app.Run();
        }
    }
}
