using Flunt.Validations;

namespace EstacionamentoContext.Domain.Comandos
{
	public abstract class BaseRegistroPlaca : BaseComando
	{
		public BaseRegistroPlaca() { }

		public BaseRegistroPlaca(string registroPlaca)
			=> RegistroPlaca = registroPlaca;

		public BaseRegistroPlaca(string registroPlaca, string cpf)
		{
			RegistroPlaca = registroPlaca;

			Cpf = cpf;
		}

		public string RegistroPlaca { get; set; } = null!;

		public string Cpf { get; set; } = null!;

		public override void Validar()
		{
			AddNotifications(
				new Contract<string>()
				.Requires()
				.IsNotNullOrEmpty(RegistroPlaca, nameof(RegistroPlaca), "Registro de placa inválida.")
				.IsNotNullOrWhiteSpace(RegistroPlaca, nameof(RegistroPlaca), "Registro de placa não pode conter espaços em branco.")
				.AreEquals(RegistroPlaca.Length, 7, nameof(RegistroPlaca), "Registro contem 7 caracteres sem ífens.")
				.NotContains(RegistroPlaca, "-", "Registro não deve conter '-'.")
			);
		}
	}
}
