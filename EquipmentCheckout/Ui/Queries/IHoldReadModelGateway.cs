using EquipmentCheckout.Ui.ReadModels;

namespace EquipmentCheckout.Ui.Queries
{
    public interface IHoldReadModelGateway
    {
        List<HoldRow> GetHoldsForItem(int itemId);
    }
}