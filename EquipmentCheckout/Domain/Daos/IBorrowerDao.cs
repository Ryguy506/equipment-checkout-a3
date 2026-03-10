using EquipmentCheckout.Domain.Entities;

namespace EquipmentCheckout.Domain.Daos
{
	public interface IBorrowerDao
	{
		Borrower? FindById(int id);
		int CountActiveLoans(int borrowerId);
	}
}
