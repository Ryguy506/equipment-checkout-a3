using EquipmentCheckout.Ui.ReadModels;

namespace EquipmentCheckout.Ui.ViewModels
{
    public class CreateHoldVm
    {
        public int EquipmentItemId { get; set; }
        public int BorrowerId { get; set; }
        public List<BorrowerOption> Borrowers { get; set; } = new();
    }
}
