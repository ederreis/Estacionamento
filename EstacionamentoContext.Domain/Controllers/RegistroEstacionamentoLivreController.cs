using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Handlers;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstacionamentoContext.Domain.Controllers
{
	[ApiController]
	public class RegistroEstacionamentoLivreController : ControllerBase
	{
		[SwaggerOperation(
			OperationId = "PostEstacionamentoLivre",
			Tags = new[] { "EstacionamentoLivre" })]
		[HttpPost("/Parametrizar/RegistroEstacionamentoLivre")]
		public IActionResult ParametrizarEstacionamentoLivre(
			[FromServices] IEstacionamentoLivreRepositorio estacionamentoLivreRepositorio, 
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio,
			[FromBody] EstacionamentoLivreModel model)
		{
			DateTime inicioVigencia;
			DateTime finalVigencia;

			if (!model.IsValid() || !DateTime.TryParse(model.HoraInicial, out inicioVigencia) || !DateTime.TryParse(model.HoraFinal, out finalVigencia))
				return BadRequest(new { Mensagem = new[] { "DiaDaSemana tem que compreender um valor entre 0 e 6, 0 = Domingo, 1 = Segunda,...", "horaInicial e horaFinal representa a hora exemplo: 13:30" } });

			var comando = new RegistrarEstacionamentoLivreComando(model.DiaDaSemana, inicioVigencia, finalVigencia);

			var handler = new ParamEstacionamentoLivreHandler(estacionamentoLivreRepositorio, aluguelVagaRepositorio);

			var retorno = handler.Handle(comando) as ResultadoComando;

			if (retorno!.Sucesso)
				return Ok(retorno);
			else
				return BadRequest(retorno);
		}
	}
}
