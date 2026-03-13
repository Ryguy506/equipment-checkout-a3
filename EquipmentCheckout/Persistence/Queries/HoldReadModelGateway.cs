using EquipmentCheckout.Ui.ReadModels;
using EquipmentCheckout.Ui.Queries;
using EquipmentCheckout.Persistence.Ef;

namespace EquipmentCheckout.Persistence.Queries
{
    public class HoldReadModelGateway : IHoldReadModelGateway
    {
        private readonly AppDbContext _db;

        public HoldReadModelGateway(AppDbContext db)
        {
            _db = db;
        }

        public List<HoldRow> GetHoldsForItem(int itemId)
        {
            return _db.Holds
                .Where(h => h.ItemId == itemId)
                .OrderBy(h => h.QueuePosition)
                .Join(
                    _db.Borrowers,
                    hold => hold.BorrowerId,
                    borrower => borrower.Id,
                    (hold, borrower) => new HoldRow
                    {
                        QueuePosition = hold.QueuePosition,
                        BorrowerName = borrower.Name,
                        DatePlaced = hold.DatePlaced
                    })
                .ToList();
        }
    }
}
