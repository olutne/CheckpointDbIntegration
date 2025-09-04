namespace Domain.Models;

public class Scooter
{
	public int Id { get; set; }
	public Brand Brand { get; set; }
	public int BatteryCapacity { get; set; }
	public Status Status { get; set; }
	public ICollection<Trip>? Trips { get; set; } // Navigation property to related trips

}