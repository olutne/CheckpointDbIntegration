using System.Runtime.InteropServices.Marshalling;
using DataAccess.Interface;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Methods;

public class ScooterDao : IScooterDao {
	public IReadOnlyList<Scooter> GetScooters()
	{
		using var db = new CheckpointDbContext();
		return db.Scooters.AsNoTracking()
			.OrderBy(s => s.Id)
			.ToList();
	}

	public IReadOnlyList<Scooter> GetScootersByStatus(Status status)
	{
		using var db = new CheckpointDbContext();
		return db.Scooters.AsNoTracking()
			.Where(s => s.Status == status)
			.OrderBy(s => s.Id)
			.ToList();
	}

	public int? GetScooterBattery(int scooterId)
	{
		using var db = new CheckpointDbContext();
		return db.Scooters.AsNoTracking()
			.Where(s => s.Id == scooterId)
			.Select(s => (int?)s.BatteryCapacity)
			.FirstOrDefault();
	}
}