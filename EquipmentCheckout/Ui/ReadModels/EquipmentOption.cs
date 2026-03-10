namespace EquipmentCheckout.Ui.ReadModels
{
	public class EquipmentOption
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public string Category { get; set; } = "";

		public bool IsActive { get; set; }
	}
}
