using EstacionamentoContext.Domain.Agregados;
using EstacionamentoContext.Services.Comandos;
using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Comandos;
using EstacionamentoContext.Shared.Handlers;
using Flunt.Notifications;

namespace EstacionamentoContext.Services.Handlers;

public class EntradaDeVeiculoHandler : Notifiable<Notification>, IHandler<RegistrarEntradaVeiculoComando>
{
	private readonly IVeiculoRepositorio _veiculoRepositorio;

	private readonly IPrecoRepositorio _precoRepositorio;

	private readonly IAluguelVagaRepositorio _aluguelVagaRepositorio;

	public EntradaDeVeiculoHandler(
		IVeiculoRepositorio veiculoRepositorio,
		IAluguelVagaRepositorio aluguelVagaRepositorio,
		IPrecoRepositorio precoRepositorio)
	{
		_veiculoRepositorio = veiculoRepositorio;

		_aluguelVagaRepositorio = aluguelVagaRepositorio;

		_precoRepositorio = precoRepositorio;
	}

	public IResultadoComando Handle(RegistrarEntradaVeiculoComando comando)
	{
		comando.Validar();

		if (!comando.IsValid)
		{
			AddNotifications(comando);

			return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
		}

		var preco = _precoRepositorio.BuscarPrecoParametrizadoPorData(DateTime.Now);

		if (preco == null)
		{
			AddNotification("Preco", "Sem registro de preço vigencia parametrizado.");

			return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
		}

		var veiculo = _veiculoRepositorio.BuscarVeiculoPorPlaca((Placa)comando.RegistroPlaca);

		if (veiculo == null)
			veiculo = new Veiculo((Placa)comando.RegistroPlaca);
		else
		{
			var contratoEmAberto = _aluguelVagaRepositorio.BuscarRegistroEmAberto(veiculo.Placa);

			if (contratoEmAberto != null)
			{
				AddNotification("Contrato", $"Contrato com veiculo já firmado em {contratoEmAberto.EntradaEm.ToShortDateString()}.");

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}
		}

		var aluguel = new AluguelVagaAgregado(veiculo);

		try
		{
			_veiculoRepositorio.SalvarVeiculo(veiculo);

			_aluguelVagaRepositorio.SalvarContrato(aluguel);

			_aluguelVagaRepositorio.Salvar();

			return new ResultadoComando(IsValid, "Registrada a entrada do veículo.");
		}
		catch (Exception ex)
		{
			AddNotification("Exception", ex.Message);

			return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
		}
	}
}
