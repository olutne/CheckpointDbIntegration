using Domain.Models;

namespace BusinessLayer.Interface;

public interface IScooterService {
	List<Scooter> GetAvailableWithBatteryOver20();
}