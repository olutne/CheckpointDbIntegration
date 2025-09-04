using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess {
	public class CheckpointDbContext : DbContext
	{
		public DbSet<Scooter> Scooters => Set<Scooter>();

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
				.HasCheckConstraint("CK_Scooter_BatteryCapacity", "\"batterycapacity\" BETWEEN 1 AND 100");
		}

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
	}
}