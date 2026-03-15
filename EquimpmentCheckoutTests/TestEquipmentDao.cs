
using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Domain.Daos;

public class TestEquipmentDao : IEquipmentDao
{
    public bool ItemIsActive = true;
    public bool ItemOnLoan = false;

    public EquipmentItem FindById(int id)
    {
        return new EquipmentItem
        {
            Id = id,
            IsActive = ItemIsActive
        };
    }

    public bool IsOnActiveLoan(int itemId)
    {
        return ItemOnLoan;
    }
}