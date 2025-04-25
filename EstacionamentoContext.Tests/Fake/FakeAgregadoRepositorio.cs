using EstacionamentoContext.Domain.Agregados;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Domain.Queries;

namespace EstacionamentoContext.Tests.Fake
{
	public class FakeAgregadoRepositorio : IAluguelVagaRepositorio
	{
		List<AluguelVagaAgregado> _agregados { get; set; } = new List<AluguelVagaAgregado>();

		public FakeAgregadoRepositorio(FakeVeiculoRepositorio _veiculosFake)
		{
			var cadastradoEm = DateTime.Now.AddHours(-10).AddMinutes(-2);

			var agregado = new AluguelVagaAgregado(_veiculosFake._veiculos.First(), cadastradoEm);

			_agregados.Add(agregado);
		}

		public AluguelVagaAgregado? BuscarRegistroEmAberto(Placa placa)
			=> _agregados.AsQueryable().FirstOrDefault(AluguelVagaAgregadoQueries.BuscarContratoNaoFinalizado(placa));

		public bool AlgumContratoEmAberto(DateTime inicio, DateTime final)
			=> _agregados.AsQueryable().Where(AluguelVagaAgregadoQueries.BuscarRegistroEmAberto(inicio, final)).Any();

		public void RegistrarAgregado(AluguelVagaAgregado abregado)
		{
			int indice = _agregados.FindIndex(x => x.Id == abregado.Id);

			if (indice == -1)
				_agregados.Add(abregado);
			else
				_agregados.Insert(indice, abregado);
		}

		public void Salvar()
		{

		}

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosEmAberto()
		{
			throw new NotImplementedException();
		}

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizados()
		{
			throw new NotImplementedException();
		}

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizadosNoDia(DateTime data)
		{
			throw new NotImplementedException();
		}

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosJaFinalizadosNosDiasAnteriores(DateTime data)
		{
			throw new NotImplementedException();
		}

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosPorCondutor(CPF cpf)
		{
			throw new NotImplementedException();
		}

		public IReadOnlyCollection<AluguelVagaAgregado?> BuscarContratosPorVeiculo(Placa placa)
		{
			throw new NotImplementedException();
		}

		public void SalvarContrato(AluguelVagaAgregado agregado)
		{

		}
	}
}
