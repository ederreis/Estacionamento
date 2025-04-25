namespace EstacionamentoContext.Services.Comandos;

public sealed class RegistrarSaidaVeiculoComando : BaseRegistroPlaca
{
	public RegistrarSaidaVeiculoComando() { }

	public RegistrarSaidaVeiculoComando(string registroPlaca, string cpf, long ticks) : base(registroPlaca, cpf)
		=> Ticks = ticks;

	public long Ticks { get; set; }
}