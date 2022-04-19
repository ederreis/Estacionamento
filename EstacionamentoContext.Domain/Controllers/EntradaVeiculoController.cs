using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Handlers;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstacionamentoContext.Domain.Controllers
{
	[ApiController]
	public class EntradaVeiculoController : ControllerBase
	{
		[SwaggerOperation(
			OperationId = "PostVeiculo",
			Tags = new[]{"Veiculo"})]
		[HttpPost("/EntradaVeiculo")]
		public IActionResult EntradaVeiculo(
			[FromServices] IVeiculoRepositorio veiculoRepositorio,
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio,
			[FromServices] IPrecoRepositorio precoRepositorio,
			[FromBody] EntradaVeiculoModel model)
		{
			var comando = new RegistrarEntradaVeiculoComando(model.RegistroPlaca);

			var handler = new EntradaDeVeiculoHandler(veiculoRepositorio, aluguelVagaRepositorio, precoRepositorio);

			var retorno = handler.Handle(comando) as ResultadoComando;

			if (retorno!.Sucesso)
				return Ok(retorno);
			else
				return BadRequest(retorno);
		}	
	}
}
