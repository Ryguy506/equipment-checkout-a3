namespace EquipmentCheckout.Ui.ReadModels
{
    public class HoldRow
    {
        public int QueuePosition { get; set; }

        public string BorrowerName { get; set; } = "";

        public DateTime DatePlaced { get; set; }
    }
}