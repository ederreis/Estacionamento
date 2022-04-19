using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Comandos;
using EstacionamentoContext.Shared.Handlers;
using Flunt.Notifications;

namespace EstacionamentoContext.Domain.Handlers
{
	public class ParamPrecoHandler : Notifiable<Notification>, IHandler<RegistrarPrecoComando>
	{
		private readonly IPrecoRepositorio _precoRepositorio;

		private readonly IAluguelVagaRepositorio _aluguelVagaRepositorio;

		public ParamPrecoHandler(IPrecoRepositorio precoRepositorio, IAluguelVagaRepositorio aluguelVagaRepositorio)
		{ 
			_precoRepositorio = precoRepositorio;

			_aluguelVagaRepositorio = aluguelVagaRepositorio;
		}

		private void InformarNotificacoes(List<KeyValuePair<string, string>> notificacoes)
		{
			foreach (var notificacao in notificacoes)
				AddNotification(notificacao.Key, notificacao.Value);
		}

		public IResultadoComando Handle(RegistrarPrecoComando comando)
		{
			comando.Validar();

			if (!comando.IsValid)
			{
				AddNotifications(comando);

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}

			var precoParametrizado = _precoRepositorio.BuscarPrecoParametrizadoPorData(comando.Inicio);

			if (precoParametrizado == null)
			{
				var valorHora = new ValorHora(comando.ValorHoraInicial, comando.ValorHoraAdicional);

				var vigencia = new ControleVigencia(comando.Inicio, comando.Final);

				precoParametrizado = new Preco(valorHora, vigencia);
			}
			else
			{
				var contratoEmAberto = _aluguelVagaRepositorio.AlgumContratoEmAberto(comando.Inicio, comando.Final);

				if (contratoEmAberto)
				{
					AddNotification("AluguelVagaAgregado", "Não é possível atualizar a parametrização, pois já existe contrato firmado.");

					return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
				}

				var alteradoAlgumParametro = false;

				if (precoParametrizado.ValorHora.Inicial != comando.ValorHoraInicial ||
					precoParametrizado.ValorHora.Adicional != comando.ValorHoraAdicional)
				{
					precoParametrizado.AlterarValorHora(new ValorHora(comando.ValorHoraInicial, comando.ValorHoraAdicional));

					alteradoAlgumParametro = true;
				}

				if (precoParametrizado.ControleVigencia.Inicio != comando.Inicio ||
					precoParametrizado.ControleVigencia.Final != comando.Final)
				{
					precoParametrizado.AlterarControleVigencia(new ControleVigencia(comando.Inicio, comando.Final));

					alteradoAlgumParametro = true;
				}

				if (!alteradoAlgumParametro)
				{
					AddNotification("ParametrizacaoPreco", "Nada para atualizar.");

					return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
				}
			}

			if (!precoParametrizado.IsValid)
				InformarNotificacoes(precoParametrizado.Notifications);

			if (!IsValid)
				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());

			try
			{
				_precoRepositorio.SalvarParametrizacao(precoParametrizado);

				_precoRepositorio.Salvar();

				return new ResultadoComando(IsValid, "Parametrizacao Salva com sucesso.");
			}
			catch (Exception ex)
			{
				AddNotification("Exception", ex.Message);

				return new ResultadoComando(IsValid, Notifications.Select(notificacao => notificacao.Message).ToArray());
			}
		}
	}
}
