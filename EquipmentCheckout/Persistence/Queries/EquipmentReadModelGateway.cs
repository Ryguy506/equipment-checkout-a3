using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Ui.Queries;
using EquipmentCheckout.Ui.ReadModels;

namespace EquipmentCheckout.Persistence.Queries
{
	public class EquipmentReadModelGateway : IEquipmentReadModelGateway
	{

		private readonly AppDbContext _db;

		public EquipmentReadModelGateway(AppDbContext db)
		{
			_db = db;
		}

		public List<EquipmentOption> GetEquipment()
		{
			return _db.EquipmentItems
				.Select(e => new EquipmentOption
				{
					Id = e.Id,
					Name = e.Name,
					Category = e.Category,
					IsActive = e.IsActive
				})
				.OrderBy(e => e.Category)
				.ThenBy(e => e.Name)
				.ToList();
		}
	}
}
