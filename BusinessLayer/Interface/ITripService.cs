using Domain.Models;

namespace BusinessLayer.Interface;

public interface ITripService
{
	List<Trip> GetTripsForUser(int userId);
}