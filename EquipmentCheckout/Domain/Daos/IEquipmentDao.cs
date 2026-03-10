using EquipmentCheckout.Domain.Entities;

namespace EquipmentCheckout.Domain.Daos
{
	public interface IEquipmentDao
	{
		EquipmentItem? FindById(int id);
		bool IsOnActiveLoan(int equipmentItemId); 
	}
}
