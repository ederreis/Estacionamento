using Flunt.Validations;

namespace EstacionamentoContext.Services.Comandos;

public sealed class RegistrarNovoCondutorCommand : BaseRegistroPlaca
{
	public RegistrarNovoCondutorCommand() { }

	public RegistrarNovoCondutorCommand(string registroPlaca, string cpf, string primeiroNome, string sobreNome, long ticks) : base(registroPlaca, cpf)
	{
		PrimeiroNome = primeiroNome;

		SobreNome = sobreNome;

		Ticks = ticks;
	}

	public string PrimeiroNome { get; set; } = null!;

	public string SobreNome { get; set; } = null!;

	public long Ticks { get; set; }

	public override void Validar()
	{
		AddNotifications(
			new Contract<string>()
			.Requires()
			.IsNotNullOrEmpty(Cpf, nameof(Cpf), "Cpf não preenchido")
			.IsNotNullOrWhiteSpace(Cpf, nameof(Cpf), "Cpf não preenchido")
			.IsTrue(ValidarCPF(), nameof(Cpf), "Cpf não é válido")
			.IsNotNullOrEmpty(PrimeiroNome, nameof(PrimeiroNome), "Primeiro nome não informado")
			.IsBetween(PrimeiroNome.Length, 3, 30, nameof(PrimeiroNome), "Primeiro nome tem que conter entre 3 e 30 caracteres.")
			.IsNotNullOrEmpty(SobreNome, nameof(SobreNome), "Sobre nome não informado")
			.IsBetween(SobreNome.Length, 3, 30, nameof(SobreNome), "Sobre nome tem que conter entre 3 e 30 caracteres.")
		);

		AddNotifications(
			new Contract<long>()
			.Requires()
			.IsGreaterOrEqualsThan(Ticks, 360000000000, "Cadastro opcional de condutor não permitido.")
		);

		base.Validar();
	}

	private bool ValidarCPF() => Cpf != null && Cpf.Length == 11 && long.TryParse(Cpf, out _);
}
