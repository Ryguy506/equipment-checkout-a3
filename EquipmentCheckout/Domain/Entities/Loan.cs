namespace EquipmentCheckout.Domain.Entities
{
	public class Loan
	{
		public int Id { get; set; }
		public int EquipmentItemId { get; set; }
		public int BorrowerId { get; set; }
		public DateTime LoanDate { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime? ReturnedDate { get; set; }

		public Loan() { }
	}
}
