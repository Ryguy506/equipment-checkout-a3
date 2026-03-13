using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Domain.Daos;

namespace EquipmentCheckout.Persistence.Daos
{
	public class HoldDao : IHoldDao
	{

		private readonly AppDbContext _db;

		public HoldDao(AppDbContext db)
		{
			_db = db;
		}

		public void Create(Hold hold)
		{
			_db.Holds.Add(hold);
			_db.SaveChanges();
		}

		public bool BorrowerHasHold(int borrowerId, int itemId)
		{
			return _db.Holds.Any(h =>
				h.BorrowerId == borrowerId &&
				h.ItemId == itemId);
		}

		public int GetNextQueuePosition(int itemId)
		{
			return _db.Holds
				.Where(h => h.ItemId == itemId)
				.Count() + 1;
		}

		public Hold? GetFirstHoldForItem(int itemId)
		{
			return _db.Holds
				.Where(h => h.ItemId == itemId)
				.OrderBy(h => h.QueuePosition)
				.FirstOrDefault();
		}

		public void Remove(int holdId)
		{
			var hold = _db.Holds.Find(holdId);

			if (hold != null)
			{
				_db.Holds.Remove(hold);
				_db.SaveChanges();
			}
		}
	}
}