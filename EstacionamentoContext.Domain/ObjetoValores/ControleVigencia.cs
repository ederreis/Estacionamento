using EstacionamentoContext.Shared.ObjetoValores;

namespace EstacionamentoContext.Domain.ObjetoValores
{
	public class ControleVigencia : ObjetoValor
	{
		private ControleVigencia() { }

		public ControleVigencia(DateTime inicio, DateTime final)
		{
			Inicio = inicio;

			Final = final;

			var tempoDecorrido = Final - Inicio;

			if (tempoDecorrido.TotalSeconds < 0)
				AddNotification("ControleVigencia", "Data ou Hora inicial não pode ser maior que a Data ou Hora final");
		}

		public DateTime Inicio { get; private set; }
		
		public DateTime Final { get; private set; }
	}
}
