using EstacionamentoContext.Services.Comandos;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoWeb.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			var a = new RegistrarEntradaVeiculoComando();
			return View();
		}
	}
}
