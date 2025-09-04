
using BusinessLayer.Interface;
using DataAccess.Interface;

namespace BusinessLayer.Service;
public class StatsService : IStatsService {
	private readonly IStatsDao _dao;

	public StatsService(IStatsDao dao)
	{
		_dao = dao;
	}

	public decimal? GetAveragePricePerKm()
		=> _dao.GetAveragePricePerKm();

	public (int userId, int tripCount)? GetUserWithMostTrips()
		=> _dao.GetUserWithMostTrips();
}

