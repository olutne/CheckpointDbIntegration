// IntegrationTests/Tests.cs
using BusinessLayer;
using BusinessLayer.Service;
using DataAccess;
using DataAccess.Methods;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace IntegrationTests {
	public class Tests {
		//[OneTimeSetUp]
		//public void Setup()
		//{
		//	DatabaseMethods.RebuildDatabase();
		//}

		[SetUp]
		public void BeforeEach()
		{
			using var db = new CheckpointDbContext();

			db.Trips.RemoveRange(db.Trips);
			db.Scooters.RemoveRange(db.Scooters);
			db.Users.RemoveRange(db.Users);
			db.SaveChanges();

			db.Users.AddRange(
				new User { Id = 1, Name = "Alice", PhoneNumber = 11111111 },
				new User { Id = 2, Name = "Bob", PhoneNumber = 22222222 }
			);

			db.Scooters.AddRange(
				new Scooter { Id = 1, Brand = Brand.Voi, BatteryCapacity = 10, Status = Status.Available },
				new Scooter { Id = 2, Brand = Brand.Tier, BatteryCapacity = 75, Status = Status.Available }  
			);

			var t0 = DateTime.UtcNow.Date.AddHours(8);
			db.Trips.AddRange(
				new Trip { Id = 1, UserId = 1, ScooterId = 1, StartTime = t0.AddMinutes(10), Distance = 10m, Cost = 50m },
				new Trip { Id = 2, UserId = 1, ScooterId = 2, StartTime = t0.AddMinutes(20), Distance =  5m, Cost = 25m },
				new Trip { Id = 3, UserId = 2, ScooterId = 1, StartTime = t0.AddMinutes(30), Distance =  5m, Cost = 35m }
			);

			db.SaveChanges();
		}

		[TearDown]
		public void AfterEach()
		{
			using var db = new CheckpointDbContext();
			// Delete in FK-safe order
			db.Trips.RemoveRange(db.Trips);
			db.Scooters.RemoveRange(db.Scooters);
			db.Users.RemoveRange(db.Users);
			db.SaveChanges();
		}

		[Test]
		public void Finds_Only_Available_WithBatteryOver20()
		{
			var dao = new ScooterDao();
			var service = new ScooterService(dao);

			var result = service.GetAvailableWithBatteryOver20();

			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result[0].Id, Is.EqualTo(2));
		}

		[Test]
		public void GetTripsForUser_Returns_OnlyUserTrips_SortedByStartTime()
		{
			var dao = new TripDao();
			var svc = new TripService(dao);

			var trips = svc.GetTripsForUser(1);

			Assert.That(trips.Count, Is.EqualTo(2));
			Assert.That(trips.All(t => t.UserId == 1), Is.True);
			Assert.That(trips.Select(t => t.Id).ToArray(), Is.EqualTo(new[] { 1, 2 })); 
		}


		[Test]
		public void GetAveragePricePerKm_Returns_GlobalRatio()
		{
			var dao = new StatsDao();
			var svc = new StatsService(dao);

			var avg = svc.GetAveragePricePerKm();

			Assert.That(avg, Is.Not.Null);
			Assert.That(avg!.Value, Is.EqualTo(110m / 20m)); 
		}

		[Test]
		public void GetUserWithMostTrips_Returns_User1_With2Trips()
		{
			var dao = new StatsDao();
			var svc = new StatsService(dao);

			var result = svc.GetUserWithMostTrips();

			Assert.That(result, Is.Not.Null);
			var (userId, tripCount) = result!.Value;
			Assert.That(userId, Is.EqualTo(1));
			Assert.That(tripCount, Is.EqualTo(2));
		}

		[Test]
		public void Cannot_Have_Two_Active_Trips_For_Same_Scooter()
		{
			using var db = new CheckpointDbContext();

			var t0 = new DateTime(2025, 1, 1, 9, 0, 0, DateTimeKind.Utc);

			// Forsøk på tur #2 som også er AKTIV (EndTime = null) for samme scooter
			db.Trips.Add(new Trip
			{
				Id = 2,
				UserId = 1,
				ScooterId = 1,  
				StartTime = t0,
				EndTime = null, 
				Distance = 0m,
				Cost = 0m
			});

			// Forvent at databasen nekter dette pga unik filtrert indeks
			Assert.That(
				() => db.SaveChanges(),
				Throws.TypeOf<DbUpdateException>()
			);
		}
	}
}
