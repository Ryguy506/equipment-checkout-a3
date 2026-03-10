using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Domain.Daos;

namespace EquipmentCheckout.Persistence.Daos

{
	public class LoanDao : ILoanDao
	{

		private readonly AppDbContext _db;

		public LoanDao(AppDbContext db)
		{
			_db = db;
		}

		public int Create(Loan loan)
		{
			_db.Loans.Add(loan);
			_db.SaveChanges();
			return loan.Id;
		}

		public Loan? FindById(int loanId)
		{
			return _db.Loans.Find(loanId);
		}

		public void MarkReturned(int loanId, DateTime returnedDate)
		{
			var loan = _db.Loans.Find(loanId);
			if (loan is not null)
			{
				loan.ReturnedDate = returnedDate;
				_db.SaveChanges();
			}
		}
	}
}
