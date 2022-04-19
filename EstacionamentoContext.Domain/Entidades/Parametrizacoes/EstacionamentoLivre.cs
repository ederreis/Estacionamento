using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Entidades;

namespace EstacionamentoContext.Domain.Entidades.Parametrizacoes
{
	public sealed class EstacionamentoLivre : Entidade
	{
		private EstacionamentoLivre() { }

		public EstacionamentoLivre(DayOfWeek diaDaSemana, ControleVigencia controleVigencia)
		{
			DiaDaSemana = diaDaSemana;

			var inicio = DateTime.MinValue;
			var final = DateTime.MinValue;

			switch (DiaDaSemana)
			{
				case DayOfWeek.Monday:
					break;
				case DayOfWeek.Tuesday:

					inicio = inicio.AddDays(1);

					break;
				case DayOfWeek.Wednesday:

					inicio = inicio.AddDays(2);

					break;
				case DayOfWeek.Thursday:

					inicio = inicio.AddDays(3);

					break;
				case DayOfWeek.Friday:

					inicio = inicio.AddDays(4);

					break;
				case DayOfWeek.Saturday:

					inicio = inicio.AddDays(5);

					break;
				case DayOfWeek.Sunday:

					inicio = inicio.AddDays(7);

					break;
				default: throw new NotImplementedException();
			}

			var horaFinal = new TimeSpan(controleVigencia.Final.Hour, controleVigencia.Final.Minute, controleVigencia.Final.Second);

			final = inicio.Date.Add(horaFinal);

			var horaInicial = new TimeSpan(controleVigencia.Inicio.Hour, controleVigencia.Inicio.Minute, controleVigencia.Inicio.Second);

			inicio = inicio.Date.Add(horaInicial);

			ControleVigencia = new ControleVigencia(inicio, final);

			AddNotifications(ControleVigencia.Notifications);
		}

		public DayOfWeek DiaDaSemana { get; private set; }

		public ControleVigencia ControleVigencia { get; private set; } = null!;

		public static QuantidadeTickDesconto ObterPeriodoEstacionamentoLivre(IReadOnlyCollection<EstacionamentoLivre> estacionamentosLivre , DateTime entrada, DateTime saida)
		{
			int qtd = 0;

			long ticks = 0;

			for (var diaEmQuestao = entrada; diaEmQuestao.Date <= saida.Date; diaEmQuestao = diaEmQuestao.AddDays(1))
			{
				var estacionamentoLivre = estacionamentosLivre.FirstOrDefault(x => x.DiaDaSemana == diaEmQuestao.DayOfWeek);

				if (estacionamentoLivre == null)
					continue;

				if (diaEmQuestao.DayOfWeek == estacionamentoLivre.DiaDaSemana)
				{
					var inicio = estacionamentoLivre.ControleVigencia.Inicio.TimeOfDay;
					var final = estacionamentoLivre.ControleVigencia.Final.TimeOfDay;

					if ((diaEmQuestao.Date < saida.Date && diaEmQuestao.Date > entrada.Date) ||
						(entrada.TimeOfDay <= final && diaEmQuestao.Date == entrada.Date) ||
						(saida.TimeOfDay >= inicio && diaEmQuestao.Date == saida.Date))
					{
						qtd++;

						ticks += (final - inicio).Ticks;
					}
				}
			}

			return new QuantidadeTickDesconto(qtd, ticks);
		}

		public struct QuantidadeTickDesconto
		{
			public QuantidadeTickDesconto(int quantidade, long ticks)
			{
				Quantidade = quantidade;

				Ticks = ticks;
			}

			public int Quantidade { get; private set; }

			public long Ticks { get; private set; }
		}
	}
}
