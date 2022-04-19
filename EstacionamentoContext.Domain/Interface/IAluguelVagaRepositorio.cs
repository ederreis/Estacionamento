using EstacionamentoContext.Domain.Agregados;
using EstacionamentoContext.Domain.ObjetoValores;

namespace EstacionamentoContext.Domain.Interface
{
	public interface IAluguelVagaRepositorio : IRepositorio<AluguelVagaAgregado>
	{
		IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosEmAberto();

		IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizados();

		IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizadosNoDia(DateTime data);

		IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizadosNosDiasAnteriores(DateTime data);

		IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosPorCondutor(CPF cpf);

		IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosPorVeiculo(Placa placa);

		void SalvarAgregado(AluguelVagaAgregado agregado);

		bool AlgumContratoEmAberto(DateTime inicio, DateTime final);

		AluguelVagaAgregado? BuscarRegistroEmAberto(Placa placa);
	}
}
