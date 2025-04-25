namespace EstacionamentoContext.Domain.ObjetoValores;

public class CPF
{
	private CPF() { }

	private CPF(string numero) => Numero = numero;

	public string Numero { get; private set; }

	public override string ToString() => Numero;

	public static implicit operator CPF(string numero) => new CPF(numero);
}
