namespace EquipmentCheckout.Domain.Entities
{
    public class Hold
    {
        public int HoldId { get; set; }
        public int ItemId { get; set; }
        public int BorrowerId { get; set; }

        public DateTime DatePlaced { get; set; }

        public int QueuePosition { get; set; }
    }
}