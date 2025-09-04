// UnitTests/StatsServiceTests.cs
using System;
using BusinessLayer.Interface;
using BusinessLayer.Service; 
using DataAccess.Interface;
using Moq;
using NUnit.Framework;

namespace UnitTests {
	public class StatsServiceTests {
		private Mock<IStatsDao> _dao = null!;
		private IStatsService _service = null!;

		[SetUp]
		public void Setup()
		{
			_dao = new Mock<IStatsDao>(MockBehavior.Strict);
			_service = new StatsService(_dao.Object);
		}

		[Test]
		public void GetAveragePricePerKm_Returns_Value_And_Verifies_Call()
		{
			// Arrange
			const decimal expected = 5.5m;
			_dao.Setup(d => d.GetAveragePricePerKm()).Returns(expected);

			// Act
			var result = _service.GetAveragePricePerKm();

			// Assert
			Assert.That(result, Is.EqualTo(expected));
			_dao.Verify(d => d.GetAveragePricePerKm(), Times.Once);
			_dao.VerifyNoOtherCalls();
		}

		[Test]
		public void GetAveragePricePerKm_Returns_Null_When_No_Data()
		{
			// Arrange
			_dao.Setup(d => d.GetAveragePricePerKm()).Returns((decimal?)null);

			// Act
			var result = _service.GetAveragePricePerKm();

			// Assert
			Assert.That(result, Is.Null);
			_dao.Verify(d => d.GetAveragePricePerKm(), Times.Once);
			_dao.VerifyNoOtherCalls();
		}

		[Test]
		public void GetUserWithMostTrips_Returns_Tuple_And_Verifies_Call()
		{
			// Arrange
			(int userId, int tripCount)? expected = (userId: 1, tripCount: 7);
			_dao.Setup(d => d.GetUserWithMostTrips()).Returns(expected);

			// Act
			var result = _service.GetUserWithMostTrips();

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result!.Value.userId, Is.EqualTo(1));
			Assert.That(result.Value.tripCount, Is.EqualTo(7));
			_dao.Verify(d => d.GetUserWithMostTrips(), Times.Once);
			_dao.VerifyNoOtherCalls();
		}

		[Test]
		public void GetUserWithMostTrips_Returns_Null_When_No_Trips()
		{
			// Arrange
			_dao.Setup(d => d.GetUserWithMostTrips()).Returns(((int userId, int tripCount)?)null);

			// Act
			var result = _service.GetUserWithMostTrips();

			// Assert
			Assert.That(result, Is.Null);
			_dao.Verify(d => d.GetUserWithMostTrips(), Times.Once);
			_dao.VerifyNoOtherCalls();
		}
	}
}
