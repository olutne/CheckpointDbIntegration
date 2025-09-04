using DataAccess.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Methods;

public class TripDao : ITripDao {
	public List<Trip> GetTripsForUser(int userId)
	{
		using var db = new CheckpointDbContext();
		return db.Trips
			.AsNoTracking()
			.Where(t => t.UserId == userId)
			.OrderBy(t => t.StartTime) 
			.ToList();
	}

	public List<Trip> GetAllTrips()
	{
		using var db = new CheckpointDbContext();
		return db.Trips.AsNoTracking()
			.OrderBy(t => t.StartTime)
			.ToList();
	}

}