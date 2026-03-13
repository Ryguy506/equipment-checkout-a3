
using EquipmentCheckout.Domain.Daos;
using EquipmentCheckout.Domain.Dtos;
using EquipmentCheckout.Domain.Dtos.Requests;
using EquipmentCheckout.Domain.Dtos.Results;
using EquipmentCheckout.Domain.Entities;
namespace EquipmentCheckout.Domain.Services
{
	public class CheckoutService : ICheckoutService
	{
		private readonly IEquipmentDao _equipmentDao;
		private readonly IBorrowerDao _borrowerDao;
		private readonly ILoanDao _loanDao;
        private readonly IHoldDao _holdDao;

        public CheckoutService(IEquipmentDao equipmentDao, IBorrowerDao borrowerDao, ILoanDao loanDao, IHoldDao holdDao)
		{
			_equipmentDao = equipmentDao;
			_borrowerDao = borrowerDao;
			_loanDao = loanDao;
			_holdDao = holdDao;
		}

		public LoanResult CreateLoan(LoanRequest request)
		{
			// Rule: duration 1..14
			if (request.DurationDays < 1 || request.DurationDays > 14)
				return LoanResult.Fail("Loan duration must be between 1 and 14 days.");

			var borrower = _borrowerDao.FindById(request.BorrowerId);
			if (borrower is null)
				return LoanResult.Fail("Borrower not found.");

			// Rule: inactive borrower cannot borrow
			if (!borrower.IsActive)
				return LoanResult.Fail("Inactive borrowers cannot borrow equipment.");

			var item = _equipmentDao.FindById(request.EquipmentItemId);
			if (item is null)
				return LoanResult.Fail("Equipment item not found.");

			// Rule: inactive item cannot be loaned
			if (!item.IsActive)
				return LoanResult.Fail("Inactive equipment items cannot be loaned.");

			// Rule: item already on loan cannot be loaned
			if (_equipmentDao.IsOnActiveLoan(item.Id))
				return LoanResult.Fail("This item is already on loan.");

			// Rule: borrower max 2 active loans
			var activeCount = _borrowerDao.CountActiveLoans(borrower.Id);
			if (activeCount >= 2)
				return LoanResult.Fail("Borrowers may have at most 2 active loans.");

			var today = DateTime.Today;
			var due = today.AddDays(request.DurationDays);
			var loan = new Loan
			{
				EquipmentItemId = item.Id,
				BorrowerId = borrower.Id,
				LoanDate = today,
				DueDate = due
			};


			var newId = _loanDao.Create(loan);
			return LoanResult.Ok(newId);
		}

		public bool ReturnLoan(int loanId, out string? error)
		{
			error = null;

			var loan = _loanDao.FindById(loanId);
			if (loan is null)
			{
				error = "Loan not found.";
				return false;
			}

			if (loan.ReturnedDate != null)
			{
				error = "This loan has already been returned.";
				return false;
			}

			var today = DateTime.Today;
			_loanDao.MarkReturned(loanId, today);

            var nextHold = _holdDao.GetFirstHoldForItem(loan.EquipmentItemId);

            if (nextHold != null)
            {
                var pendingLoan = new Loan
                {
                    EquipmentItemId = loan.EquipmentItemId,
                    BorrowerId = nextHold.BorrowerId,
                    LoanDate = today,
                    DueDate = today.AddDays(2)
                };

                _loanDao.Create(pendingLoan);

                _holdDao.Remove(nextHold.ItemId);
            }

            return true;
        }

        public HoldResult PlaceHold(HoldRequest request)
        {
            var borrower = _borrowerDao.FindById(request.BorrowerId);
            if (borrower is null)
                return HoldResult.Fail("Borrower not found.");

            if (!borrower.IsActive)
                return HoldResult.Fail("Inactive borrowers cannot place holds.");

            var item = _equipmentDao.FindById(request.EquipmentItemId);
            if (item is null)
                return HoldResult.Fail("Equipment item not found.");

            if (_equipmentDao.IsOnActiveLoan(request.EquipmentItemId))
                return HoldResult.Fail("Borrower already has this item on loan.");

            if (_holdDao.BorrowerHasHold(request.BorrowerId, request.EquipmentItemId))
                return HoldResult.Fail("Borrower already has a hold on this item.");

            var queuePos = _holdDao.GetNextQueuePosition(request.EquipmentItemId);

            var hold = new Hold
            {
                BorrowerId = request.BorrowerId,
                ItemId = request.EquipmentItemId,
                DatePlaced = DateTime.Today,
                QueuePosition = queuePos
            };

            _holdDao.Create(hold);

            return HoldResult.Ok();
        }
    }
}