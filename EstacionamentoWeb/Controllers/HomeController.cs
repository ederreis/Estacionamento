//using Estacionamento.Infra.Repositorios.Parametrizacoes;
using EstacionamentoContext.Domain.Comandos;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoWeb.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			var a = new RegistrarEntradaVeiculoComando();
			//var b = new PrecoRepositorio()
			return View();
		}
	}
}
