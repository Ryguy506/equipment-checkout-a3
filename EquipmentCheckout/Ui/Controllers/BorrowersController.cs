using EquipmentCheckout.Ui.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentCheckout.Ui.Controllers
{
	public class BorrowersController : Controller
	{
		private readonly IBorrowerReadModelGateway _borrowerGateway;

		public BorrowersController(IBorrowerReadModelGateway borrowerGateway)
		{
			_borrowerGateway = borrowerGateway;
		}
		public IActionResult Index()
		{
			var borrowers = _borrowerGateway.GetBorrowers();
			return View(borrowers);
		}
	}
}
