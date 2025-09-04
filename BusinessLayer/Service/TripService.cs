using BusinessLayer.Interface;
using DataAccess.Interface;
using Domain.Models;

namespace BusinessLayer.Service;

public class TripService : ITripService {
	private readonly ITripDao _dao;

	public TripService(ITripDao dao)
	{
		_dao = dao;
	}

	public List<Trip> GetTripsForUser(int userId)
	{
		return _dao.GetTripsForUser(userId).ToList();
	}
}