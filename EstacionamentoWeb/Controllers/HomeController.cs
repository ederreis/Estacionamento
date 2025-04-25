using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoWeb.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
