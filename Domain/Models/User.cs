namespace Domain.Models;

public class User
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public int PhoneNumber { get; set; }
	public ICollection<Trip>? Trips { get; set; } 
}