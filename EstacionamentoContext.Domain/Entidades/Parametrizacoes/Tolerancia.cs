namespace EstacionamentoContext.Domain.Entidades.Parametrizacoes;

public sealed class Tolerancia
{
	public Tolerancia()
	{

	}

	public int Inicial { get; private set; } = 30;

	public int Adicional { get; private set; } = 10;

	public long InicialTicks { get { return TimeSpan.FromMinutes(Inicial).Ticks; } }

	public long AdicionalTicks { get { return TimeSpan.FromMinutes(Adicional).Ticks; } }

	public long HoraReferenciaTicks { get { return TimeSpan.FromMinutes(60).Ticks; } }
}