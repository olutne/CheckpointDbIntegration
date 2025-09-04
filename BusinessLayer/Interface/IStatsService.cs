namespace BusinessLayer.Interface;
public interface IStatsService {
	decimal? GetAveragePricePerKm();
	(int userId, int tripCount)? GetUserWithMostTrips();
}
