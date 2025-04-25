using EstacionamentoContext.Shared.ObjetoValores;

namespace EstacionamentoContext.Domain.ObjetoValores;

public class Nome : ObjetoValor
{
	private Nome() { }

	public Nome(string primeiroNome, string sobreNome)
	{
		PrimeiroNome = primeiroNome;

		SobreNome = sobreNome;
	}

	public string PrimeiroNome { get; private set; }

	public string SobreNome { get; private set; }

	public override string ToString()
		=> $"{PrimeiroNome} {SobreNome}";
}
