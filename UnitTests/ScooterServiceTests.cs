// UnitTests/ScooterServiceTests.cs
using BusinessLayer;
using BusinessLayer.Interface;
using DataAccess.Interface;
using Domain;
using Domain.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Service;

namespace UnitTests {
	public class ScooterServiceTests {
		private Mock<IScooterDao> _dao = null!;
		private IScooterService _service = null!;

		[SetUp]
		public void Setup()
		{
			_dao = new Mock<IScooterDao>(MockBehavior.Strict);
			_service = new ScooterService(_dao.Object);
		}

		[Test]
		public void Returns_only_available_with_battery_over_20()
		{
			var data = new[]
			{
				new Scooter { Id = 1, Status = Status.Available, BatteryCapacity = 10 },
				new Scooter { Id = 2, Status = Status.Available, BatteryCapacity = 25 },
				new Scooter { Id = 3, Status = Status.Available, BatteryCapacity = 90 },
				new Scooter { Id = 4, Status = Status.Available, BatteryCapacity = 20 }, // grense: ekskluderes
                new Scooter { Id = 5, Status = Status.InUse,    BatteryCapacity = 99 }, // feil status
            };

			_dao.Setup(d => d.GetScootersByStatus(Status.Available))
				.Returns(data.Where(s => s.Status == Status.Available).ToList());

			var result = _service.GetAvailableWithBatteryOver20();

			Assert.That(result.Select(x => x.Id), Is.EqualTo(new[] { 2, 3 }));
			_dao.Verify(d => d.GetScootersByStatus(Status.Available), Times.Once);
			_dao.VerifyNoOtherCalls();
		}

		[Test]
		public void Empty_when_no_available()
		{
			_dao.Setup(d => d.GetScootersByStatus(Status.Available))
				.Returns(new List<Scooter>());

			var result = _service.GetAvailableWithBatteryOver20();

			Assert.That(result, Is.Empty);
			_dao.VerifyAll();
		}

		[Test]
		public void Boundary_excludes_20_includes_21()
		{
			var data = new[]
			{
				new Scooter { Id = 10, Status = Status.Available, BatteryCapacity = 20 },
				new Scooter { Id = 11, Status = Status.Available, BatteryCapacity = 21 },
			};

			_dao.Setup(d => d.GetScootersByStatus(Status.Available))
				.Returns(data.ToList());

			var result = _service.GetAvailableWithBatteryOver20();

			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result[0].Id, Is.EqualTo(11));
			_dao.VerifyAll();
		}

		[Test]
		public void Orders_by_id_ascending()
		{
			var data = new[]
			{
				new Scooter { Id = 5, Status = Status.Available, BatteryCapacity = 99 },
				new Scooter { Id = 2, Status = Status.Available, BatteryCapacity = 35 },
				new Scooter { Id = 9, Status = Status.Available, BatteryCapacity = 50 },
			};

			_dao.Setup(d => d.GetScootersByStatus(Status.Available))
				.Returns(data.ToList());

			var result = _service.GetAvailableWithBatteryOver20();

			Assert.That(result.Select(x => x.Id).ToArray(), Is.EqualTo(new[] { 2, 5, 9 }));
			_dao.VerifyAll();
		}
	}
}
