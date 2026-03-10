using Microsoft.AspNetCore.Mvc;

namespace EquipmentCheckout.Ui.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
