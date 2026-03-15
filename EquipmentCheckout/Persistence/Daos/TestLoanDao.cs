using EquipmentCheckout.Persistence.Ef;
using EquipmentCheckout.Domain.Entities;
using EquipmentCheckout.Domain.Daos;

public class TestLoanDao : ILoanDao
{
    public List<int> ActiveItemIds = new();
    public List<Loan> Loans = new();

    public int Create(Loan loan)
    {
        loan.Id = Loans.Count + 1;
        Loans.Add(loan);
        ActiveItemIds.Add(loan.EquipmentItemId);
        return loan.Id;
    }

    public Loan? FindById(int id)
    {
        return Loans.FirstOrDefault(l => l.Id == id);
    }

    public void MarkReturned(int loanId, DateTime date)
    {
        var loan = FindById(loanId);
        if (loan != null)
        {
            loan.ReturnedDate = date;
            ActiveItemIds.Remove(loan.EquipmentItemId);
        }
    }

    public bool BorrowerHasItemOnLoan(int borrowerId, int itemId)
    {
        return Loans.Any(l =>
            l.BorrowerId == borrowerId &&
            l.EquipmentItemId == itemId &&
            l.ReturnedDate == null);
    }

    public List<Loan> GetExpiredLoans(DateTime today)
    {
        return Loans
            .Where(l => l.ReturnedDate == null && l.DueDate < today)
            .ToList();
    }
}