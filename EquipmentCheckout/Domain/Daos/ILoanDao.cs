using EquipmentCheckout.Domain.Entities;
namespace EquipmentCheckout.Domain.Daos
{
	public interface ILoanDao
	{
		int Create(Loan loan);
		Loan? FindById(int loanId);
		void MarkReturned(int loanId, DateTime returnedDate);
	}
}
