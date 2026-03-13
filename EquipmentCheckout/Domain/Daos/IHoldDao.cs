using EquipmentCheckout.Domain.Entities;
namespace EquipmentCheckout.Domain.Daos
{
    public interface IHoldDao
    {
        void Create(Hold hold);
        bool BorrowerHasHold(int borrowerId, int itemId);
        int GetNextQueuePosition(int itemId);
        Hold? GetFirstHoldForItem(int itemId);
        void Remove(int holdId);
    }
}