namespace EquipmentCheckout.Ui.ReadModels
{
	public class LoanRow
	{
		public int LoanId { get; set; }
		public string EquipmentName { get; set; } = "";
		public string BorrowerName { get; set; } = "";

		public DateTime LoanDate { get; set; }  
		public DateTime DueDate { get; set; }
	}
}
