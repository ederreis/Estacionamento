using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Handlers;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstacionamentoContext.Domain.Controllers
{
	[ApiController]
	public class RegistroPrecoController : ControllerBase
	{
		[SwaggerOperation(
			OperationId = "PostPreco",
			Tags = new[] { "Preco" })]
		[HttpPost("/Parametrizar/RegistroPreco")]
		public IActionResult ParametrizarPreco(
			[FromServices] IPrecoRepositorio precoRepositorio,
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio,
			[FromBody] PrecoModel model)
		{
			DateTime inicioVigencia;
			DateTime finalVigencia;

			if (!DateTime.TryParse(model.DataInicioVigencia, out inicioVigencia) || !DateTime.TryParse(model.DataFinalVigencia, out finalVigencia))
				return BadRequest(new { Mensagem = new[] { "inicioVigencia e finalVigencia é uma data exemplo: 01/01/2022" } });

			var comando = new RegistrarPrecoComando(model.ValorHoraInicial, model.ValorHoraAdicional, inicioVigencia, finalVigencia);

			var handler = new ParamPrecoHandler(precoRepositorio, aluguelVagaRepositorio);

			var retorno = handler.Handle(comando) as ResultadoComando;

			if (retorno!.Sucesso)
				return Ok(retorno);
			else
				return BadRequest(retorno);
		}
	}
}
