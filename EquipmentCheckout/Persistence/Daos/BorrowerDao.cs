using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Domain.Daos;

namespace EquipmentCheckout.Persistence.Daos
{
	public class BorrowerDao : IBorrowerDao
	{
		private readonly AppDbContext _db;

		public BorrowerDao(AppDbContext db)
		{
			_db = db;
		}

		public Borrower? FindById(int id)
		{
			return _db.Borrowers.Find(id);
		}

		public int CountActiveLoans(int borrowerId)
		{
			return _db.Loans.Count(l => l.BorrowerId == borrowerId && l.ReturnedDate == null);
		}
	}
}
