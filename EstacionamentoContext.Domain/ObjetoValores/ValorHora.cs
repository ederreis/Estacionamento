using EstacionamentoContext.Shared.ObjetoValores;

namespace EstacionamentoContext.Domain.ObjetoValores;

public class ValorHora : ObjetoValor
{
	private ValorHora() { }

	public ValorHora(decimal inicial, decimal adicional)
	{
		Inicial = inicial;

		Adicional = adicional;

		if (Inicial < 0)
			AddNotification(nameof(Inicial), "Valor da Hora Inicial não pode ser um número negativo.");

		if (Adicional < 0)
			AddNotification(nameof(Adicional), "Valor da Hora Adicional não pode ser um número negativo.");
	}

	public decimal Inicial { get; private set; }

	public decimal Adicional { get; private set; }
}
