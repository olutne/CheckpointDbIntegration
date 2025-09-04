// UnitTests/TripServiceTests.cs
using System.Collections.Generic;
using System.Linq;
using BusinessLayer;
using BusinessLayer.Interface;
using BusinessLayer.Service;
using DataAccess.Interface;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace UnitTests {
	public class TripServiceTests {
		private Mock<ITripDao> _dao = null!;
		private ITripService _service = null!;

		[SetUp]
		public void Setup()
		{
			_dao = new Mock<ITripDao>(MockBehavior.Strict);
			_service = new TripService(_dao.Object);
		}

		[Test]
		public void GetTripsForUser_CallsDaoAndReturnsResult()
		{
			// Arrange
			var userId = 42;
			var data = new List<Trip>
			{
				new Trip { Id = 10, UserId = userId },
				new Trip { Id = 11, UserId = userId }
			};

			_dao.Setup(d => d.GetTripsForUser(userId))
				.Returns(data);

			// Act
			var result = _service.GetTripsForUser(userId);

			// Assert
			Assert.That(result.Count, Is.EqualTo(2));
			Assert.That(result.Select(t => t.Id).ToArray(), Is.EqualTo(new[] { 10, 11 }));

			_dao.Verify(d => d.GetTripsForUser(userId), Times.Once);
			_dao.VerifyNoOtherCalls();
		}
	}
}