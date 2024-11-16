using Store.G04.APIs.MiddeelWare;
using Store.G04.Ropository.Data.Contexts;
using Store.G04.Ropository.Data;
using Microsoft.EntityFrameworkCore;
using Store.G04.Ropository.Identity.Contexts;
using Store.G04.Ropository.Identity;
using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Entities.Identity;

namespace Store.G04.APIs.Helper
{
    public static class ConfigureMiddleWare
    {
        public static async Task<WebApplication> ConfigreMiddleWaresAsync(this WebApplication app) 
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreDbContext>();
            var identitycontext = services.GetRequiredService<StoreIdentityDbContext>();
            var userManger = services.GetRequiredService<UserManager<AppUser>>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
                await identitycontext.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManger);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There Are Problem During Apply Migrations ");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseMiddleware<ExceptionMiddleWare>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            return app;
        } 
    }
}
