namespace EstacionamentoContext.Domain.Comandos
{
	public sealed class RegistrarEntradaVeiculoComando : BaseRegistroPlaca
	{
		public RegistrarEntradaVeiculoComando() { }

		public RegistrarEntradaVeiculoComando(string RegistroPlaca) : base(RegistroPlaca)
		{

		}
	}
}