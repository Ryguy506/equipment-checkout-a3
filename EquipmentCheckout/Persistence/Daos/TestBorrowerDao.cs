using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Domain.Daos;

public class TestBorrowerDao : IBorrowerDao
{
    public bool IsActive = true;
    public int ActiveLoanCount = 0;

    public Borrower FindById(int id)
    {
        return new Borrower
        {
            Id = id,
            IsActive = IsActive
        };
    }

    public int CountActiveLoans(int borrowerId)
    {
        return ActiveLoanCount;
    }
}