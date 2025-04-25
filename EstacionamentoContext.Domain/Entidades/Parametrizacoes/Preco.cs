using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Entidades;

namespace EstacionamentoContext.Domain.Entidades.Parametrizacoes;

public sealed class Preco : Entidade
{
	private Preco() { }

	public Preco(ValorHora valorHora, ControleVigencia controleVigencia)
	{
		ValorHora = valorHora;

		ControleVigencia = controleVigencia;

		if (!ControleVigencia.IsValid)
			AddNotifications(controleVigencia.Notifications);
	}

	public ValorHora ValorHora { get; private set; } = null!;

	public ControleVigencia ControleVigencia { get; private set; } = null!;

	public void AlterarValorHora(ValorHora valorHora)
		=> ValorHora = valorHora;

	public void AlterarControleVigencia(ControleVigencia controleVigencia)
		=> ControleVigencia = controleVigencia;
}
