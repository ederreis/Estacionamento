using Estacionamento.Infra.Contexto;
using EstacionamentoContext.Domain.Agregados;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Domain.Queries;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infra.Repositorios.Agregado
{
	public class AluguelVagaRepositorio :
		Repositorio<AluguelVagaAgregado>,
		IAluguelVagaRepositorio
	{
		public AluguelVagaRepositorio(EstacionamentoContexto contexto) : base(contexto)
		{
		}

		public bool AlgumContratoEmAberto(DateTime inicio, DateTime final)
			=> _contexto.Agregados.AsQueryable().Where(AluguelVagaAgregadoQueries.BuscarRegistroEmAberto(inicio, final)).Any();

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosEmAberto()
			=> _contexto.Agregados.AsQueryable().Include(x => x.Veiculo)
				.Where(AluguelVagaAgregadoQueries.BuscarContratosEmAberto())
				.AsNoTracking()
				.AsEnumerable()
				.ToArray();

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizados()
			=> _contexto.Agregados.AsQueryable().Include(x => x.Veiculo)
				.Where(AluguelVagaAgregadoQueries.BuscarContratosFinalizados())
				.AsNoTracking()
				.AsEnumerable()
				.ToArray();

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizadosNoDia(DateTime data)
			=> _contexto.Agregados.AsQueryable().Include(x => x.Veiculo)
				.Where(AluguelVagaAgregadoQueries.BuscarContratosFinalizadosNoDia(data))
				.AsNoTracking()
				.AsEnumerable()
				.ToArray();

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizadosNosDiasAnteriores(DateTime data)
			=> _contexto.Agregados.AsQueryable().Include(x => x.Veiculo)
				.Where(AluguelVagaAgregadoQueries.BuscarContratosFinalizadosAnteriormente(data))
				.AsNoTracking()
				.AsEnumerable()
				.ToArray();

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosPorCondutor(CPF cpf)
			=> _contexto.Agregados.AsQueryable().Include(x => x.Veiculo).Include(x => x.Condutor)
				.Where(AluguelVagaAgregadoQueries.BuscarHistorico(cpf))
				.AsNoTracking()
				.AsEnumerable()
				.ToArray();

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosPorVeiculo(Placa placa)
			=> _contexto.Agregados.AsQueryable().Include(x => x.Veiculo).Include(x => x.Condutor)
				.Where(AluguelVagaAgregadoQueries.BuscarHistorico(placa))
				.AsNoTracking()
				.AsEnumerable()
				.ToArray();

		public AluguelVagaAgregado? BuscarRegistroEmAberto(Placa placa)
			=> _contexto.Agregados.AsQueryable()
			.Include(x => x.Veiculo)
			.SingleOrDefault(AluguelVagaAgregadoQueries.BuscarContratoNaoFinalizado(placa));

		public void SalvarAgregado(AluguelVagaAgregado agregado)
		{
			if (agregado.Id == 0)
				_contexto.Agregados.Add(agregado);
			else
				_contexto.Agregados.Update(agregado);
		}
	}
}
