using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Entidades;

namespace EstacionamentoContext.Domain.Entidades.Bem
{
	public class Veiculo : Entidade
	{
		private Veiculo() { }

		public Veiculo(Placa placa) : base()
			=> Placa = placa;

		public Placa Placa { get; private set; } = null!;
	}
}
