using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.Entidades.Cliente;
using EstacionamentoContext.Shared.Entidades;

namespace EstacionamentoContext.Domain.Agregados;

public class AluguelVagaAgregado : Entidade
{
	private AluguelVagaAgregado() { }

#if DEBUG
	public AluguelVagaAgregado(Veiculo veiculo, DateTime dataHora) : base(dataHora) => Veiculo = veiculo;
#endif

	public AluguelVagaAgregado(Veiculo veiculo) : base() => Veiculo = veiculo;

	public int VeiculoId { get; private set; }

	public virtual Veiculo Veiculo { get; private set; } = null!;

	public int? CondutorId { get; private set; }

	public virtual Condutor? Condutor { get; private set; }

	public DateTime EntradaEm { get { return CadastradoEm; } }

	public DateTime? FinalizadoEm { get; private set; }

	public void CadastrarCondutor(Condutor condutor)
	{
		FinalizadoEm = DateTime.Now;

		if (condutor == null)
		{
			AddNotification(nameof(Condutor), $"Não é possível vincular o condutor ao veículo {Veiculo.Placa.Registro}, pois o condutor é um objeto nulo.");

			return;
		}

		Condutor = condutor;

		AddNotifications(condutor.Notifications);
	}

	public void FinalizarContrato() => FinalizadoEm = DateTime.Now;
}
