using AutoMapper;
using EmployeeCRUD.Core.Reopsitories;
using EmployeeCRUD.Core.Services.Contracts.Genaric;
using EmployeeCRUD.Infrastructure.DataAccess;
using EmployeeCRUD.Infrastructure.DataAccess.Repositories;
using EmployeeCRUD.Infrastructure.MappMagicStorerofiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EmployeeCRUD.Infrastructure
{
    public static class DepndancyInjection
    {
        public static void AddCoreRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddAutoMapper(typeof(EmployeeProfile).Assembly);
        }
        public static IdentityBuilder AddIdentityStores(this IdentityBuilder builder)
        {
            return builder.AddEntityFrameworkStores<ApplicationDbContext>();
        }
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                context.Database.Migrate();
            }
            catch
            {
                Console.WriteLine("Db migration failed! or can not build application.");
                throw;
            }

            return host;
        }
    }
}
