using EquipmentCheckout.Ui.ReadModels;

namespace EquipmentCheckout.Ui.ViewModels
{
	public class NewLoanVm
	{
		public List<BorrowerOption> Borrowers { get; set; } = new();
		public List<EquipmentOption> Equipment { get; set; } = new();

		public int BorrowerId { get; set; }
		public int EquipmentItemId { get; set; }
		public int DurationDays { get; set; } = 7;

		public string? ErrorMessage { get; set; }


	}
}
