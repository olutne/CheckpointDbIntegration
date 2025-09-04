// DataAccess/Methods/StatsDao.cs
using System.Linq;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Methods {
	public class StatsDao : IStatsDao {
		public decimal? GetAveragePricePerKm()
		{
			using var db = new CheckpointDbContext();

			// Ta med kun trips med Distance > 0 for å unngå deling på 0
			var q = db.Trips.AsNoTracking().Where(t => t.Distance > 0);

			var totalKm = q.Sum(t => t.Distance);
			if (totalKm == 0) return null;

			var totalCost = q.Sum(t => t.Cost);
			return totalCost / totalKm;
		}

		public (int userId, int tripCount)? GetUserWithMostTrips()
		{
			using var db = new CheckpointDbContext();

			var grp = db.Trips.AsNoTracking()
				.GroupBy(t => t.UserId)
				.Select(g => new { UserId = g.Key, TripCount = g.Count() })
				.OrderByDescending(x => x.TripCount)
				.ThenBy(x => x.UserId) // deterministisk ved likt antall
				.FirstOrDefault();

			if (grp is null) return null;
			return (grp.UserId, grp.TripCount);
		}
	}
}