using Domain.Models;

namespace DataAccess.Interface;

public interface ITripDao
{
	List<Trip> GetTripsForUser(int userId);
}