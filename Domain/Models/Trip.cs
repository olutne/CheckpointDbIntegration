namespace Domain.Models {
	public class Trip {
		public int Id { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		public decimal Distance { get; set; }

		public decimal Cost { get; set; }

		public int ScooterId { get; set; }
		public Scooter? Scooter { get; set; }

		public int UserId { get; set; }
		public User? User { get; set; }

	}
}