// BusinessLayer/ScooterService.cs
using BusinessLayer.Interface;
using DataAccess.Interface;
using Domain;
using Domain.Models;

namespace BusinessLayer.Service {
	public class ScooterService : IScooterService {
		private readonly IScooterDao _dao;

		public ScooterService(IScooterDao dao)
		{
			_dao = dao;
		}

		public List<Scooter> GetAvailableWithBatteryOver20()
		{
			var available = _dao.GetScootersByStatus(Status.Available);
			return available
				.Where(s => s.BatteryCapacity > 20)
				.OrderBy(s => s.Id)
				.ToList();
		}
	}
}