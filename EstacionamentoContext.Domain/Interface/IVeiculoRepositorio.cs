using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.ObjetoValores;

namespace EstacionamentoContext.Domain.Interface;

public interface IVeiculoRepositorio : IRepositorio<Veiculo>
{
	Veiculo? BuscarVeiculoPorPlaca(Placa placa);

	void SalvarVeiculo(Veiculo veiculo);
}
