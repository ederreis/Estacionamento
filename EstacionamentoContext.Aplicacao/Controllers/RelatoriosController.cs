using EstacionamentoContext.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstacionamentoContext.Aplicacao.Controllers
{
	[ApiController]
	public class RelatoriosController : ControllerBase
	{
		[SwaggerOperation(
			OperationId = "GetContratosEmAberto",
			Tags = new[] { "RelatorioContratosEmAberto" })]
		[HttpGet("/Relatorios/VeiculosEmAberto")]
		public IActionResult VeiculosEmAberto(
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio)
		{
			var contratosEmAberto = aluguelVagaRepositorio.BuscarContratosEmAberto();

			if (contratosEmAberto == null || contratosEmAberto.Count == 0)
				return Ok(new { });

			return Ok(contratosEmAberto.Select(contrato => new
			{
				Veiculo = contrato!.Veiculo.Placa.Registro
			}));
		}

		[SwaggerOperation(
			OperationId = "GetContratosJaFinalizados",
			Tags = new[] { "RelatorioContratosJaFinalizados" })]
		[HttpGet("/Relatorios/VeiculosJaFinalizados")]
		public IActionResult VeiculosJaFinalizados(
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio)
		{
			var contratosJaFinalizados = aluguelVagaRepositorio.BuscarContratosJaFinalizados();

			if (contratosJaFinalizados == null || contratosJaFinalizados.Count == 0)
				return Ok(new { });

			return Ok(contratosJaFinalizados.Select(contrato => new
			{
				Veiculo = contrato!.Veiculo.Placa.Registro
			}));
		}

		[SwaggerOperation(
			OperationId = "GetContratosFinalizadosNoDia",
			Tags = new[] { "RelatorioContratosFinalizadosNoDia" })]
		[Route("/Relatorios/HistoricoVeiculosFinalizadosNoDia/{data:datetime}")]
		[HttpGet]
		public IActionResult HistoricoVeiculosFinalizadosNoDia(
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio,
			[FromRoute] DateTime data)
		{
			var contratosFinalizadosNoDia = aluguelVagaRepositorio.BuscarContratosJaFinalizadosNoDia(data);

			if (contratosFinalizadosNoDia == null || contratosFinalizadosNoDia.Count == 0)
				return Ok(new { });

			return Ok(contratosFinalizadosNoDia.Select(contrato => new
			{
				Veiculo = contrato!.Veiculo.Placa.Registro
			}));
		}

		[SwaggerOperation(
			OperationId = "GetContratosFinalizadosNosDiasAnteriores",
			Tags = new[] { "RelatorioContratosFinalizadosNosDiasAnteriores" })]
		[Route("/Relatorios/HistoricoVeiculosFinalizadosNosDiasAnteriores/{data:datetime}")]
		[HttpGet]
		public IActionResult HistoricoVeiculosFinalizadosNosDiasAnteriores(
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio,
			[FromRoute] DateTime data)
		{
			var contratosFinalizadosNosDiasAnteriores = aluguelVagaRepositorio.BuscarContratosJaFinalizadosNosDiasAnteriores(data);

			if (contratosFinalizadosNosDiasAnteriores == null || contratosFinalizadosNosDiasAnteriores.Count == 0)
				return Ok(new { });

			return Ok(contratosFinalizadosNosDiasAnteriores.Select(contrato => new
			{
				Veiculo = contrato!.Veiculo.Placa.Registro
			}));
		}

		[SwaggerOperation(
			OperationId = "GetHistoricoEstacionamentoPorCondutor",
			Tags = new[] { "HistoricoEstacionamentoPorCondutor" })]
		[Route("/Relatorios/HistoricoEstacionamentoPorCondutor/{cpf}")]
		[HttpGet]
		public IActionResult HistoricoEstacionamentoPorCondutor(
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio,
			[FromRoute] string cpf)
		{
			if (cpf == null || cpf.Length != 11)
				return BadRequest(new { Mensagem = new[] { "CPF inválido" } });

			var historicoEstacionamentoPorCondutor = aluguelVagaRepositorio.BuscarContratosPorCondutor(cpf);

			if (historicoEstacionamentoPorCondutor == null || historicoEstacionamentoPorCondutor.Count == 0)
				return Ok(new { });

			return Ok(historicoEstacionamentoPorCondutor.Select(contrato => new
			{
				Veiculo = contrato!.Veiculo.Placa.Registro,
				CPF = contrato!.Condutor!.Cpf.Numero,
				Nome = contrato!.Condutor.Nome.ToString()
			}));
		}

		[SwaggerOperation(
			OperationId = "GetHistoricoEstacionamentoPorVeiculo",
			Tags = new[] { "HistoricoEstacionamentoPorVeiculo" })]
		[Route("/Relatorios/HistoricoEstacionamentoPorVeiculo/{placa}")]
		[HttpGet]
		public IActionResult HistoricoEstacionamentoPorVeiculo(
			[FromServices] IAluguelVagaRepositorio aluguelVagaRepositorio,
			[FromRoute] string placa)
		{
			if (placa == null || placa.Length != 7)
				return BadRequest(new { Mensagem = new[] { "Placa inválida" } });

			var historicoEstacionamentoPorCondutor = aluguelVagaRepositorio.BuscarContratosPorVeiculo(placa);

			if (historicoEstacionamentoPorCondutor == null || historicoEstacionamentoPorCondutor.Count == 0)
				return Ok(new { });

			return Ok(historicoEstacionamentoPorCondutor.Select(contrato => new
			{
				Veiculo = contrato!.Veiculo.Placa.Registro,
				CPF = contrato!.Condutor!.Cpf.Numero,
				Nome = contrato!.Condutor.Nome.ToString()
			}));
		}
	}
}
