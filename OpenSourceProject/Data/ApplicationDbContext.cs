using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OpenSourceProject.Models;
using System.Reflection.Emit;

namespace OpenSourceProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
           
            builder.Entity<ProviderService>().HasKey(PS => new { PS.ProviderId,PS.SubServiceId });
            builder.Entity<ProviderBrand>().HasKey(pb => new { pb.ProviderId, pb.BrandId });
            builder.Entity<Product>().Property(p => p.Image).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject < List<string>>(v) );


            builder.Entity<Car>().Navigation("CarType").AutoInclude();
            builder.Entity<Car>().Navigation("CarModel").AutoInclude();
            builder.Entity<Product>().Navigation("Category").AutoInclude();

            builder .Entity<Provider>().Navigation(p=>p.ProviderBrands).AutoInclude();
            builder.Entity<Provider>().Navigation(p => p.ProviderServices).AutoInclude();
          //  builder.Entity<Provider>().Navigation(p => p.ProviderLocation).AutoInclude();
            builder.Entity<Provider>().Navigation(p => p.ProviderWorkHours).AutoInclude();
            builder.Entity<Service>().Navigation(s=>s.SubServices).AutoInclude();
            builder.Entity<ProviderBrand>().Navigation(p => p.Brand).AutoInclude();
            builder.Entity<Provider>().Navigation(p => p.ProviderProducts).AutoInclude();





        }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Provider> providers { get; set; }

        public DbSet<CarModel> carModels { get; set; }
        public DbSet<CarType> carTypes { get; set; }
        public DbSet<Car> cars { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<SubService> subServices { get; set; }

        public DbSet<Brand> brands { get; set; }
        public DbSet<ProviderWorkHour> providerWorkHours { get; set; }
       // public DbSet<ProviderLocation> providerLocations { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> Product { get; set; }    

         public DbSet<UserAddress> addresses { get; set; }
        public DbSet<Order>orders { get; set; } 
        
    }
}
