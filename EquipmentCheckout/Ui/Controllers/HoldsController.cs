using EquipmentCheckout.Domain.Dtos;
using EquipmentCheckout.Domain.Dtos.Requests;
using EquipmentCheckout.Domain.Services;
using EquipmentCheckout.Ui.Queries;
using EquipmentCheckout.Ui.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentCheckout.Ui.Controllers
{
    public class HoldsController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private readonly IBorrowerReadModelGateway _borrowerQueries;
        private readonly IHoldReadModelGateway _holdQueries;

        public HoldsController(ICheckoutService checkoutService, IBorrowerReadModelGateway borrowerQueries, IHoldReadModelGateway holdQueries)
        {
            _checkoutService = checkoutService;
            _borrowerQueries = borrowerQueries;
            _holdQueries = holdQueries;

        }



		[HttpGet]
		public IActionResult Item (int itemId)
        {
            var holds = _holdQueries.GetHoldsForItem(itemId);
            return View(holds);
		}

		[HttpGet]

		public IActionResult Create(int itemId)
        {
            var borrowers = _borrowerQueries.GetBorrowers();

            var vm = new CreateHoldVm
            {
                EquipmentItemId = itemId,
                Borrowers = borrowers
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CreateHoldVm vm)
        {
            var request = new HoldRequest(vm.BorrowerId, vm.EquipmentItemId);

            var result = _checkoutService.PlaceHold(request);

            if (!result.Success)
            {   
				vm.ErrorMessage = result.Error;
				vm.Borrowers = _borrowerQueries.GetBorrowers();
                return View(vm);
            }

            return Redirect($"/holds/item?itemId={vm.EquipmentItemId}");
        }
    }
}