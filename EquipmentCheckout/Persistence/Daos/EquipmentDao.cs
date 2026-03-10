using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Domain.Daos;


namespace EquipmentCheckout.Persistence.Daos
{
	public class EquipmentDao : IEquipmentDao
	{
		private readonly AppDbContext _db;

		public EquipmentDao(AppDbContext db)
		{
			_db = db;
		}

		public EquipmentItem? FindById(int id)
		{
			return _db.EquipmentItems.Find(id);
		}

		public bool IsOnActiveLoan(int equipmentItemId)
		{
			return _db.Loans.Any(l => l.EquipmentItemId == equipmentItemId && l.ReturnedDate == null);
		}
}
}
