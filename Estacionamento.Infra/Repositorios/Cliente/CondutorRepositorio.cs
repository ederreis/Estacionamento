using Estacionamento.Infra.Contexto;
using EstacionamentoContext.Domain.Entidades.Cliente;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Domain.Queries;

namespace Estacionamento.Infra.Repositorios.Cliente
{
	public class CondutorRepositorio :
		Repositorio<Condutor>,
		ICondutorRepositorio
	{
		public CondutorRepositorio(EstacionamentoContexto contexto) : base(contexto)
		{
		}

		public Condutor? BuscarCondutorPorCpf(CPF cpf)
			=> _contexto.Condutores
				.AsQueryable()
				.FirstOrDefault(CondutorQueries.BuscaPorCpf(cpf));

		public IEnumerable<Condutor?> BuscarCondutorPorPlaca(Placa placa)
			=> _contexto.Agregados
				.AsQueryable()
				.Where(AluguelVagaAgregadoQueries.BuscarAluguelVagaPorPlacaComCondutor(placa))
				.DistinctBy(x => x.Condutor!.Id)
				.Select(x => x.Condutor);
		

		public void SalvarCondutor(Condutor condutor)
		{
			if (condutor.Id == 0)
				_contexto.Condutores.Add(condutor);
			else
				_contexto.Condutores.Update(condutor);
		}
	}
}
