using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentCheckout.Domain.Entities
{
    public class Hold
    {
        [Column("Id")]
		public int HoldId { get; set; }

        [Column("EquipmentItemId")]
		public int ItemId { get; set; }
        public int BorrowerId { get; set; }

        public DateTime DatePlaced { get; set; }

        public int QueuePosition { get; set; }
    }
}