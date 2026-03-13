namespace EquipmentCheckout.Domain.Dtos
{
    public class HoldRequest
    {
        public int BorrowerId { get; set; }
        public int EquipmentItemId { get; set; }

        public HoldRequest(int borrowerId, int equipmentItemId)
        {
            BorrowerId = borrowerId;    
            EquipmentItemId = equipmentItemId;
        }   
    }
}