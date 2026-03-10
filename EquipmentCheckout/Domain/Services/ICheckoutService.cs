using EquipmentCheckout.Domain.Dtos.Requests;
using EquipmentCheckout.Domain.Dtos.Results;

namespace EquipmentCheckout.Domain.Services
{
	public interface ICheckoutService
	{
		LoanResult CreateLoan(LoanRequest request);
		bool ReturnLoan(int loanId, out string? error);
	}
}
