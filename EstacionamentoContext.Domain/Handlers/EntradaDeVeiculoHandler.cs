using EstacionamentoContext.Domain.Agregados;
using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Comandos;
using EstacionamentoContext.Shared.Handlers;
using Flunt.Notifications;

namespace EstacionamentoContext.Domain.Handlers
{
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
				AddNotification("Preco", "Sem registro tabela preço vigencia parametrizado.");

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
					AddNotification("Contrato", $"Contrato com veiculo já firmado em {contratoEmAberto.EntradaEm}.");

					return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
				}
			}

			var aluguel = new AluguelVagaAgregado(veiculo);

			try
			{
				_veiculoRepositorio.SalvarVeiculo(veiculo);

				_aluguelVagaRepositorio.SalvarAgregado(aluguel);

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
}
