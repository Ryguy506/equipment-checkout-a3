namespace EquipmentCheckout.Domain.Entities
{
	public class Borrower
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string StudentNumber { get; set; }
		public bool IsActive { get; set; }
	}
}
