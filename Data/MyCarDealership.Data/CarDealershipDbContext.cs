namespace MyCarDealership.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Models;

    public class CarDealershipDbContext : IdentityDbContext<ApplicationUser>
    {
        public CarDealershipDbContext(DbContextOptions<CarDealershipDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Extra> Extras { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<ExtraType> ExtraTypes { get; set; }

        public DbSet<CarExtra> CarExtras { get; set; }

        public DbSet<FuelType> FuelTypes { get; set; }

        public DbSet<TransmissionType> TransmissionTypes { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Car>()
                .HasOne(c => c.FuelType)
                .WithMany(ft => ft.Cars)
                .HasForeignKey(c => c.FuelTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Car>()
                .HasOne(c => c.TransmissionType)
                .WithMany(tt => tt.Cars)
                .HasForeignKey(c => c.TransmissionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Car>()
                .HasOne(c => c.Post)
                .WithOne(p => p.Car)
                .HasForeignKey<Post>(p => p.CarId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Entity<Car>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            builder
                .Entity<Post>()
                .HasOne(p => p.Creator)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Creator)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Extra>()
                .HasOne(e => e.Type)
                .WithMany(et => et.Extras)
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<CarExtra>()
                .HasKey(ce => new { ce.CarId, ce.ExtraId });

            builder
                .Entity<CarExtra>()
                .HasOne(ce => ce.Car)
                .WithMany(c => c.CarExtras)
                .HasForeignKey(ce => ce.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<CarExtra>()
                .HasOne(ce => ce.Extra)
                .WithMany(e => e.CarExtras)
                .HasForeignKey(ce => ce.ExtraId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
