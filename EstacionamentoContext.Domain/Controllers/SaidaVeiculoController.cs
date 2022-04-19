using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Handlers;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstacionamentoContext.Domain.Controllers
{
	[ApiController]
	public class SaidaVeiculoController : ControllerBase
	{
		[SwaggerOperation(
			OperationId = "PostSaidaVeiculo",
			Tags = new[] { "SaidaVeiculo" })]
		[HttpPost("/SaidaVeiculo")]
		public IActionResult SaidaVeiculo(
			[FromServices] ICondutorRepositorio condutorRepositorio,
			[FromServices] IAluguelVagaRepositorio eventoRepositorio,
			[FromBody] SaidaVeiculoModel model)
		{
			var comando = new RegistrarSaidaVeiculoComando(model.RegistroPlaca, model.CpfCondutor, model.Ticks);

			var handler = new SaidaDeVeiculoHandler(condutorRepositorio, eventoRepositorio, null!);

			var retorno = handler.Handle(comando) as ResultadoComando;

			if (retorno!.Sucesso)
				return Ok(retorno);
			else
				return BadRequest(retorno);
		}

		[SwaggerOperation(
			OperationId = "PostCadastroCondutorComCadastroCondutor",
			Tags = new[] { "SaidaVeiculoComCadastroCondutor" })]
		[HttpPost("/SaidaVeiculoComCadastroCondutor")]
		public IActionResult SaidaVeiculoComCadastroCondutor(
			[FromServices] ICondutorRepositorio condutorRepositorio,
			[FromServices] IAluguelVagaRepositorio eventoRepositorio,
			[FromServices] IVeiculoRepositorio veiculoRepositorio,
			[FromBody] SaidaVeiculoModel model)
		{
			var comando = new RegistrarNovoCondutorCommand(model.RegistroPlaca, model.CpfCondutor, model.PrimeiroNome, model.SobreNome, model.Ticks);

			var handler = new SaidaDeVeiculoHandler(condutorRepositorio, eventoRepositorio, veiculoRepositorio);

			var retorno = handler.Handle(comando) as ResultadoComando;

			if (retorno!.Sucesso)
				return Ok(retorno);
			else
				return BadRequest(retorno);
		}
	}
}