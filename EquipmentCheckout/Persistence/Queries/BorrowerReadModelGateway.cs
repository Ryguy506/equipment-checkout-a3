
using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Ui.Queries;
using EquipmentCheckout.Ui.ReadModels;

namespace EquipmentCheckout.Persistence.Queries
{
	public class BorrowerReadModelGateway : IBorrowerReadModelGateway
	{
		private readonly AppDbContext _db;
		public BorrowerReadModelGateway(AppDbContext db)
		{
			_db = db;
		}
		public List<BorrowerOption> GetBorrowers()
		{
			return _db.Borrowers
				.Select(b => new BorrowerOption
				{
					Id = b.Id,
					Name = b.Name,
					StudentNumber = b.StudentNumber,
					IsActive = b.IsActive
				})
				.ToList();
		}
}
}
