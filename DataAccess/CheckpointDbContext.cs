using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess {
	public class CheckpointDbContext : DbContext
	{
		public DbSet<Scooter> Scooters => Set<Scooter>();

		public DbSet<User> Users => Set<User>();
		public DbSet<Trip> Trips => Set<Trip>();


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost:5434;" +
			                         "Username=postgres;" +
			                         "Password=postgres;" +
			                         "Database=scooter;")
				.UseLowerCaseNamingConvention();
		}




		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// ----- User -----
			modelBuilder.Entity<User>(b =>
			{
				b.Property(x => x.Name).IsRequired().HasMaxLength(200);
				b.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(32);
				b.HasIndex(x => x.PhoneNumber).IsUnique();
			});

			// ----- Scooter -----
			modelBuilder.Entity<Scooter>()
				.ToTable(t =>
				{
					t.HasCheckConstraint("CK_Scooter_BatteryCapacity", "\"batterycapacity\" BETWEEN 1 AND 100");
				});

			// ----- Trip -----
			modelBuilder.Entity<Trip>(b =>
			{
				b.Property(x => x.StartTime).IsRequired();

				// Presisjon: km med to desimaler (0.01 km) og kostnad 2 desimaler
				b.Property(x => x.Distance).HasPrecision(9, 2);
				b.Property(x => x.Cost).HasPrecision(10, 2);

				// Ikke-negative verdier
				b.ToTable(t =>
				{
					t.HasCheckConstraint("CK_Trip_DistanceNonNegative", "\"distance\" >= 0");
					t.HasCheckConstraint("CK_Trip_CostNonNegative", "\"cost\" >= 0");
				});

			});
		}
	}
}