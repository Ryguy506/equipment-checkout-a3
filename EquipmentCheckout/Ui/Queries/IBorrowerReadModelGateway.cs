using EquipmentCheckout.Ui.ReadModels;
namespace EquipmentCheckout.Ui.Queries
{
	public interface IBorrowerReadModelGateway
	{
		List<BorrowerOption> GetBorrowers();
	}
}
