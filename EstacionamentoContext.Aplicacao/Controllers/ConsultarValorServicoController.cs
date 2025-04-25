using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstacionamentoContext.Aplicacao.Controllers;

[ApiController]
public class ConsultarValorServicoController : ControllerBase
{
	[SwaggerOperation(
		OperationId = "PostConsultarValorServico",
		Tags = new[] { "ConsultarValorServico" })]
	[HttpPost("/Consulta/ConsultarValorDoServico")]
	public IActionResult ConsultarValorDoServico(
		[FromServices] ICondutorRepositorio condutorRepositorio,
		[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio,
		[FromServices] IPrecoRepositorio precoRepositorio,
		[FromServices] IEstacionamentoLivreRepositorio estacionamentoLivreRepositorio,
		[FromBody] ConsultaValorModel model)
	{
		if (string.IsNullOrEmpty(model.RegistroPlaca))
			return BadRequest(new { Mensagem = new[] { "Informe a placa do veículo para fazer a pesquisa." } });

		var contrato = aluguelVagaRepositorio.BuscarRegistroEmAberto(model.RegistroPlaca);

		if (contrato == null)
			return BadRequest(new { Mensagem = new[] { $"Não encontrado contrato com o veículo informado, {model.RegistroPlaca}." } });

		var tempoEstacionadoTicks = (DateTime.Now - contrato.EntradaEm).Ticks;

		var tempoADescontar = EstacionamentoLivre.ObterPeriodoEstacionamentoLivre(estacionamentoLivreRepositorio.ListarEstacionamentoLivre(), contrato.EntradaEm, DateTime.Now);

		var totalDeHorasEstacionadas = TimeSpan.FromTicks(tempoEstacionadoTicks - tempoADescontar.Ticks);

		decimal cobrarValor = 0;

		var preco = precoRepositorio.BuscarPrecoParametrizadoPorData(contrato.EntradaEm)!;

		var tolerancia = new Tolerancia();

		if (totalDeHorasEstacionadas.Ticks <= tolerancia.InicialTicks)
		{
			if (tempoADescontar.Quantidade == 0 && preco.ValorHora.Inicial > 0)
				cobrarValor = preco.ValorHora.Inicial / 2;
			else
				cobrarValor = preco.ValorHora.Inicial;
		}
		else
		{
			var tempoDecorrido = TimeSpan.FromTicks(totalDeHorasEstacionadas.Ticks);

			int totalHorasAdicional = 0;

			if (tempoDecorrido.Hours <= 1 && tempoDecorrido.Minutes <= tolerancia.Adicional)
				totalHorasAdicional = 0;
			else if (tempoDecorrido.Hours <= 1 && tempoDecorrido.Minutes > tolerancia.Adicional)
				totalHorasAdicional = tempoDecorrido.Hours;
			else if (tempoDecorrido.Minutes <= tolerancia.Adicional)
				totalHorasAdicional = tempoDecorrido.Hours - 1;
			else
				totalHorasAdicional = tempoDecorrido.Hours;

			cobrarValor = (totalHorasAdicional * preco.ValorHora.Adicional) + preco.ValorHora.Inicial;
		}

		if (!string.IsNullOrEmpty(model.CpfCondutor))
		{
			var condutor = condutorRepositorio.BuscarCondutorPorCpf(model.CpfCondutor);

			if (condutor != null && condutor.AptoDesconto)
					cobrarValor = cobrarValor / 2;
		}

		return Ok(new ValorServicoModel(model.RegistroPlaca,
										cobrarValor,
										totalDeHorasEstacionadas.Ticks,
										preco.ValorHora.Inicial,
										preco.ValorHora.Adicional,
										tempoEstacionadoTicks));
	}
}