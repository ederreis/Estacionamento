using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Comandos;
using EstacionamentoContext.Shared.Handlers;
using Flunt.Notifications;

namespace EstacionamentoContext.Domain.Handlers
{
	public class ParamEstacionamentoLivreHandler : Notifiable<Notification>,
		IHandler<RegistrarEstacionamentoLivreComando>
	{
		private readonly IEstacionamentoLivreRepositorio _estacionamentoLivreRepositorio;

		private readonly IAluguelVagaRepositorio _aluguelVagaRepositorio;

		public ParamEstacionamentoLivreHandler(
			IEstacionamentoLivreRepositorio estacionamentoLivreRepositorio,
			IAluguelVagaRepositorio aluguelVagaRepositorio)
		{ 
			_estacionamentoLivreRepositorio = estacionamentoLivreRepositorio;

			_aluguelVagaRepositorio = aluguelVagaRepositorio;
		}

		private void InformarNotificacoes(List<KeyValuePair<string, string>> notificacoes)
		{ 
			foreach (var notificacao in notificacoes)
				AddNotification(notificacao.Key, notificacao.Value);
		}

		public IResultadoComando Handle(RegistrarEstacionamentoLivreComando comando)
		{
			comando.Validar();

			if (!comando.IsValid)
			{
				AddNotifications(comando);

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}

			var diaDaSemana = (DayOfWeek)comando.DiaDaSemana;

			var controleVigencia = new ControleVigencia(comando.Inicio, comando.Final);

			var estacionamentoLivre = _estacionamentoLivreRepositorio.ListarEstacionamentoLivre()
				.FirstOrDefault(x => x.DiaDaSemana == diaDaSemana);

			if (estacionamentoLivre == null)
				estacionamentoLivre = new EstacionamentoLivre(diaDaSemana, controleVigencia);
			else
				estacionamentoLivre!.MudarControleVigencia(controleVigencia);

			if (!estacionamentoLivre.IsValid)
				InformarNotificacoes(estacionamentoLivre.Notifications);
				
			if (!IsValid)
				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());

			try
			{
				_estacionamentoLivreRepositorio.SalvarEstacionamentoLivre(estacionamentoLivre);

				return new ResultadoComando(IsValid, "Estacionamento livre atualizada com sucesso.");
			}
			catch (Exception ex)
			{
				AddNotification(nameof(Exception), ex.Message);

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}
		}
	}
}
