
using EmployeeCRUD.Core.Entities;
using EmployeeCRUD.Core.Services.Contracts;
using EmployeeCRUD.Entities.Users;
using EmployeeCRUD.Infrastructure.Extentions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeCRUD.Infrastructure.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
                                                            ApplicationRole,
                                                            int,
                                                            ApplicationUserClaim,
                                                            ApplicationUserRole,
                                                            ApplicationUserLogin,
                                                            ApplicationRoleClaims,
                                                            ApplicationUserToken


                                                            >
    {
        private readonly IUserDataProvider userDataProvider;

        //-----------------------------------------------------------------------------------------
        public ApplicationDbContext()
        {
        }
        //-----------------------------------------------------------------------------------------
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IUserDataProvider userDataProvider)
             : base(options)
        {
            this.userDataProvider = userDataProvider;
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Get all types ins the project that implements entity configurations 
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        // ------------------------------------------------------------------------ ------------
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
            optionsBuilder.EnableSensitiveDataLogging();
        }
        // ------------------------------------------------------------------------ ------------
        public override int SaveChanges()
        {
            // Track Entity Auditing
            ChangeTracker.TrackAuditing(this.userDataProvider);
            return base.SaveChanges();
        }
        // ------------------------------------------------------------------------ ------------
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Track Entity Auditing
            ChangeTracker.TrackAuditing(this.userDataProvider);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        // ------------------------------------------------------------------------ ------------
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Track Entity Auditing
            ChangeTracker.TrackAuditing(this.userDataProvider);
            return base.SaveChangesAsync(cancellationToken);
        }
        // ------------------------------------------------------------------------ ------------
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new ApplicationDbContext(builder.Options, null);
        }
    }
}
