using Flunt.Validations;

namespace EstacionamentoContext.Domain.Comandos
{
	public sealed class RegistrarEstacionamentoLivreComando : BaseComando
	{
		public RegistrarEstacionamentoLivreComando() { }

		public RegistrarEstacionamentoLivreComando(sbyte diaDaSemana, DateTime inicio, DateTime final)
		{
			DiaDaSemana = diaDaSemana;

			Inicio = inicio;
			
			Final = final;
		}

		public sbyte DiaDaSemana { get; set; }

		public DateTime Inicio { get; set; }

		public DateTime Final { get; set; }

		public override void Validar()
		{
			AddNotifications(
				new Contract<sbyte>()
				.Requires()
				.IsBetween(DiaDaSemana, 0, 6, "O dia da semana tem que compreender um valor entre 0 e 6, 0 = Domingo, 1 = Segunda...")
				);

			var tempoDecorrido = Final - Inicio;

			AddNotifications(
				new Contract<DateTime>()
				.Requires()
				.IsFalse(tempoDecorrido.TotalSeconds < 0, "ControleVigencia", "Data ou Hora inicial não pode ser maior que a Data ou Hora final")
			);
		}
	}
}