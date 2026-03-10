namespace EquipmentCheckout.Domain.Dtos.Requests
{
	public class LoanRequest
	{
		public int BorrowerId { get; set; }
		public int EquipmentItemId { get; set; }
		public int DurationDays { get; set; }

		public LoanRequest(int borrowerId, int equipmentItemId, int durationDays)
		{
			BorrowerId = borrowerId;
			EquipmentItemId = equipmentItemId;
			DurationDays = durationDays;
		}
	}
}
