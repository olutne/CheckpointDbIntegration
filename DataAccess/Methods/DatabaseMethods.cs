using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Methods;

public class DatabaseMethods
{
	public static void RebuildDatabase()
	{
		using CheckpointDbContext db = new();
		db.Database.EnsureDeleted();

		db.Database.Migrate();
	}

	public static void ClearDatabase()
	{
		using var db = new CheckpointDbContext();
		using var tx = db.Database.BeginTransaction();

		db.Set<User>().ExecuteDelete(); 
		db.Set<Scooter>().ExecuteDelete();
		db.Set<Trip>().ExecuteDelete();

		tx.Commit();
	}
}