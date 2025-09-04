using Domain;
using Domain.Models;

namespace DataAccess.Interface;

public interface IScooterDao {
	IReadOnlyList<Scooter> GetScooters();
	IReadOnlyList<Scooter> GetScootersByStatus(Status status);
	int? GetScooterBattery(int scooterId);
}