using EstacionamentoContext.Domain.Entidades.Cliente;
using EstacionamentoContext.Domain.ObjetoValores;

namespace EstacionamentoContext.Domain.Interface
{
	public interface ICondutorRepositorio : IRepositorio<Condutor>
	{
		IEnumerable<Condutor?> BuscarCondutorPorPlaca(Placa placa);

		Condutor? BuscarCondutorPorCpf(CPF cpf);

		void SalvarCondutor(Condutor condutor);
	}
}