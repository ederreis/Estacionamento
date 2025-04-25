using EstacionamentoContext.Shared.ObjetoValores;

namespace EstacionamentoContext.Domain.ObjetoValores;

public class Placa : ObjetoValor
{
	private Placa() { }

	private Placa(string registro) => Registro = registro;

	public string Registro { get; private set; }

	public override string ToString() => Registro;

	public static implicit operator Placa(string registro) => new Placa(registro.ToUpper());
}
