using EquipmentCheckout.Domain.Dtos;
using EquipmentCheckout.Domain.Dtos.Requests;
using EquipmentCheckout.Domain.Services;
using EquipmentCheckout.Ui.Queries;
using EquipmentCheckout.Ui.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentCheckout.Ui.Controllers;

public class LoansController : Controller
{
	private readonly ILoanReadModelGateway _loanGateway;
	private readonly IEquipmentReadModelGateway _equipmentGateway;
	private readonly IBorrowerReadModelGateway _borrowerGateway;
	private readonly ICheckoutService _checkoutService;

	public LoansController(
		ILoanReadModelGateway loanGateway,
		IEquipmentReadModelGateway equipmentGateway,
		IBorrowerReadModelGateway borrowerGateway,
		ICheckoutService checkoutService)
	{
		_loanGateway = loanGateway;
		_equipmentGateway = equipmentGateway;
		_borrowerGateway = borrowerGateway;
		_checkoutService = checkoutService;
	}

	[HttpGet]
	public IActionResult Index()
	{
		var loans = _loanGateway.GetActiveLoans();
		return View(loans);
	}

	[HttpGet]
	public IActionResult New()
	{
		var vm = new NewLoanVm();
		vm = BuildNewLoanVm(vm);
		return View(vm);
	}

	[HttpPost]
	public IActionResult New(NewLoanVm vm)
	{
		if (vm.BorrowerId <= 0 || vm.EquipmentItemId <= 0)
		{
			vm = BuildNewLoanVm(vm);
			vm.ErrorMessage = "Please select a borrower and an equipment item.";
			return View(vm);
		}

		var request = new LoanRequest(vm.BorrowerId, vm.EquipmentItemId, vm.DurationDays);
		var result = _checkoutService.CreateLoan(request);

		if (!result.Success)
		{
			vm = BuildNewLoanVm(vm);
			vm.ErrorMessage = result.Error;
		
			return View(vm);
		}

		 TempData["Success"] = $"Loan #{result.LoanId} created successfully.";

		return RedirectToAction("Index");
	}

	[HttpPost]
	public IActionResult Return(int loanId)
	{
		 var ok = _checkoutService.ReturnLoan(loanId, out var error);

		if (!ok)
		{
			TempData["Error"] = error;
		}

		return RedirectToAction("");
	}

	private NewLoanVm BuildNewLoanVm(NewLoanVm vm)
	{
		
		vm.Borrowers = _borrowerGateway.GetBorrowers();
		vm.Equipment = _equipmentGateway.GetEquipment();

		return vm;
	}
}