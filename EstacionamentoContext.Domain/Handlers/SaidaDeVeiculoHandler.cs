using EstacionamentoContext.Domain.Agregados;
using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Entidades.Cliente;
using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Comandos;
using EstacionamentoContext.Shared.Handlers;
using Flunt.Notifications;

namespace EstacionamentoContext.Domain.Handlers
{
	public class SaidaDeVeiculoHandler : Notifiable<Notification>, IHandler<RegistrarSaidaVeiculoComando>, IHandler<RegistrarNovoCondutorCommand>
	{
		private readonly ICondutorRepositorio _condutorRepositorio;

		private readonly IAluguelVagaRepositorio _eventoRepositorio;
		
		private readonly IVeiculoRepositorio _veiculoRepositorio;

		public SaidaDeVeiculoHandler(
			ICondutorRepositorio condutorRepositorio,
			IAluguelVagaRepositorio eventoRepositorio,
			IVeiculoRepositorio veiculoRepositorio)
		{
			_condutorRepositorio = condutorRepositorio;

			_eventoRepositorio = eventoRepositorio;

			_veiculoRepositorio = veiculoRepositorio;
		}

		private C? ValidacaoPadrao<C, T>(T comando)
			where T : BaseRegistroPlaca
			where C : AluguelVagaAgregado
		{
			comando.Validar();

			if (!comando.IsValid)
			{
				AddNotifications(comando);

				return null;
			}

			var registroPlaca = (Placa)comando!.RegistroPlaca;

			var contratoAluguelEmAberto = _eventoRepositorio.BuscarRegistroEmAberto(registroPlaca);

			if (contratoAluguelEmAberto == null)
			{
				AddNotification("Contrato", $"Não existe registro em aberto para o veículo {registroPlaca.Registro}");

				return null;
			}

			return contratoAluguelEmAberto as C;
		}

		public IResultadoComando Handle(RegistrarSaidaVeiculoComando comando)
		{
			var contratoAluguelEmAberto = ValidacaoPadrao<AluguelVagaAgregado, RegistrarSaidaVeiculoComando>(comando); // ValidacaoPadrao

			if (contratoAluguelEmAberto == null)
			{
				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}

			bool consumirDesconto = comando.Ticks > 0;

			if (!string.IsNullOrEmpty(comando.Cpf) && consumirDesconto)
			{
				var condutor = _condutorRepositorio.BuscarCondutorPorCpf(comando.Cpf);

				if (condutor != null && condutor.AptoDesconto)
				{
					condutor.ConsumirDesconto();

					_condutorRepositorio.SalvarCondutor(condutor);
				}
			}

			contratoAluguelEmAberto.FinalizarContrato();

			try
			{
				_eventoRepositorio.SalvarAgregado(contratoAluguelEmAberto);

				_eventoRepositorio.Salvar();

				return new ResultadoComando(IsValid, "Registrada a saída do veículo.");
			}
			catch (Exception ex)
			{
				AddNotification("Exception", ex.Message);

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}
		}

		public IResultadoComando Handle(RegistrarNovoCondutorCommand comando)
		{
			var contratoAluguelEmAberto = ValidacaoPadrao<AluguelVagaAgregado, RegistrarNovoCondutorCommand>(comando);

			if (contratoAluguelEmAberto == null)
				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());

			if (comando.Ticks < TimeSpan.FromHours(10).Ticks)
			{
				AddNotification("CadastroCondutor", "Cadastro de condutor não disponível, somente a cada 10 horas.");

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}

			var condutor = _condutorRepositorio.BuscarCondutorPorCpf(comando.Cpf);

			if (condutor == null)
			{
				condutor = new Condutor(
					comando.Cpf,
					new Nome(comando.PrimeiroNome, comando.SobreNome),
					contratoAluguelEmAberto.Veiculo);
			}
			else
			{
				if (condutor.AptoDesconto)
					condutor.ConsumirDesconto();
			}

			contratoAluguelEmAberto.CadastrarCondutor(condutor);

			if (!contratoAluguelEmAberto.IsValid)
			{
				foreach(var notificacao in contratoAluguelEmAberto.Notifications)
					AddNotification(notificacao.Key, notificacao.Value);

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}
				
			contratoAluguelEmAberto.FinalizarContrato();

			try
			{
				_condutorRepositorio.SalvarCondutor(condutor);

				_veiculoRepositorio.SalvarVeiculo(contratoAluguelEmAberto.Veiculo);

				_eventoRepositorio.SalvarAgregado(contratoAluguelEmAberto);

				_eventoRepositorio.Salvar();

				return new ResultadoComando(IsValid, "Registrada a saída do veículo.");
			}
			catch (Exception ex)
			{
				AddNotification("Exception", ex.Message);

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}
		}
	}
}