using EquipmentCheckout.Ui.ReadModels;

namespace EquipmentCheckout.Ui.Queries
{
	public interface ILoanReadModelGateway
	{
		List<LoanRow> GetActiveLoans();
	}
}
