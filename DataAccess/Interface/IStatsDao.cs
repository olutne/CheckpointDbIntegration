namespace DataAccess.Interface;

public interface IStatsDao {

	decimal? GetAveragePricePerKm();
	(int userId, int tripCount)? GetUserWithMostTrips();
}