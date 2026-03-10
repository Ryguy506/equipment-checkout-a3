using EquipmentCheckout.Ui.ReadModels;
namespace EquipmentCheckout.Ui.Queries
{
	public interface IEquipmentReadModelGateway
	{
		List<EquipmentOption> GetEquipment();
	}
}
