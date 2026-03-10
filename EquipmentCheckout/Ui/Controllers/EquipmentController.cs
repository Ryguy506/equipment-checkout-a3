using Microsoft.AspNetCore.Mvc;
using EquipmentCheckout.Ui.Queries;

namespace EquipmentCheckout.Ui.Controllers
{
	public class EquipmentController : Controller
	{
		private readonly IEquipmentReadModelGateway _equipmentGateway;

		public EquipmentController(IEquipmentReadModelGateway equipmentGateway)
		{
			_equipmentGateway = equipmentGateway;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var equipment = _equipmentGateway.GetEquipment();
			return View(equipment);
		}
	}
}
