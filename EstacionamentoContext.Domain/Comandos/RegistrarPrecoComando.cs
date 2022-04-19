using Flunt.Validations;

namespace EstacionamentoContext.Domain.Comandos
{
	public sealed class RegistrarPrecoComando : BaseComando
	{
		public RegistrarPrecoComando() { }

		public RegistrarPrecoComando(decimal valorHoraInicial, decimal valorHoraAdicional, DateTime inicio, DateTime final)
		{
			ValorHoraInicial = valorHoraInicial;
			ValorHoraAdicional = valorHoraAdicional;
			Inicio = inicio;
			Final = final;
		}

		public decimal ValorHoraInicial { get; set; }

		public decimal ValorHoraAdicional { get; set; }

		public DateTime Inicio { get; set; }

		public DateTime Final { get; set; }

		public override void Validar()
		{
			AddNotifications(
				new Contract<decimal>()
				.Requires()
				.IsGreaterOrEqualsThan(ValorHoraInicial, 0, "Valor da hora inicial não pode ser um número negativo.")
				.IsGreaterOrEqualsThan(ValorHoraAdicional, 0, "Valor da hora adicional não pode ser um número negativo.")
			);
						
			if ((Inicio != DateTime.MinValue ^ Final != DateTime.MinValue) ||
				(Inicio == DateTime.MinValue && Final == DateTime.MinValue)) 
			{
				AddNotification("Vigencia", "Deve ser informada valores válidos em vigencia inicial e final.");
			}
		}
	}
}
