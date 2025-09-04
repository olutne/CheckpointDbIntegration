namespace Domain;

public class TripStatsDto {
	public int TripCount { get; init; }
	public decimal? LongestKm { get; init; }
	public decimal? ShortestKm { get; init; }
	public decimal? AverageKm { get; init; }
}