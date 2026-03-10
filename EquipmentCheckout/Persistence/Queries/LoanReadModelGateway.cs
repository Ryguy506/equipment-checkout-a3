using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Ui.Queries;
using EquipmentCheckout.Ui.ReadModels;


namespace EquipmentCheckout.Persistence.Queries
{
	public class LoanReadModelGateway : ILoanReadModelGateway
	{
		private readonly AppDbContext _db;
		public LoanReadModelGateway(AppDbContext db)
		{
			_db = db;
		}
		public List<LoanRow> GetActiveLoans()
		{
			var query =
		from l in _db.Loans
		join b in _db.Borrowers on l.BorrowerId equals b.Id
		join e in _db.EquipmentItems on l.EquipmentItemId equals e.Id
		where l.ReturnedDate == null
		orderby l.DueDate
		select new LoanRow
		{
			LoanId = l.Id,
			BorrowerName = b.Name,
			EquipmentName = e.Name,
			LoanDate = l.LoanDate,
			DueDate = l.DueDate
		};

			return query.ToList();
		}


	}
}
