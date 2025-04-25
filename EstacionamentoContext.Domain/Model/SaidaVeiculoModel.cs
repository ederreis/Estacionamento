namespace EstacionamentoContext.Domain.Model;

public class SaidaVeiculoModel
{
	public string RegistroPlaca { get; set; } = string.Empty;

	public string CpfCondutor { get; set; } = string.Empty;

	public string PrimeiroNome { get; set; } = string.Empty;

	public string SobreNome { get; set; } = string.Empty;

	public long Ticks { get; set; }
}
