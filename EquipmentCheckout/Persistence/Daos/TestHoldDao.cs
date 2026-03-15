using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Domain.Daos;

public class TestHoldDao : IHoldDao
{
    private List<Hold> _holds = new();

    public void Create(Hold hold)
    {
        hold.QueuePosition = GetNextQueuePosition(hold.ItemId);
        _holds.Add(hold);
    }

    public bool BorrowerHasHold(int borrowerId, int itemId)
    {
        return _holds.Any(h => h.BorrowerId == borrowerId && h.ItemId == itemId);
    }

    public int GetNextQueuePosition(int itemId)
    {
        return _holds.Count(h => h.ItemId == itemId) + 1;
    }

    public Hold? GetFirstHoldForItem(int itemId)
    {
        return _holds
            .Where(h => h.ItemId == itemId)
            .OrderBy(h => h.QueuePosition)
            .FirstOrDefault();
    }

    public void Remove(int holdId)
    {
        var first = _holds
            .OrderBy(h => h.QueuePosition)
            .FirstOrDefault();

        if (first != null)
            _holds.Remove(first);
    }
}