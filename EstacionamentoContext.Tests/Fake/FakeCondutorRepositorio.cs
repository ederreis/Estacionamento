using EstacionamentoContext.Domain.Entidades.Cliente;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;

namespace EstacionamentoContext.Tests.Fake
{
	public class FakeCondutorRepositorio : ICondutorRepositorio
	{
		public List<Condutor> _condutores { get; private set; } = new List<Condutor>();

		public FakeCondutorRepositorio(FakeVeiculoRepositorio fakeVeiculoRepositorio)
		{
			var cpf = (CPF)"01234567891";

			_condutores.Add(
				new Condutor((CPF)"01234567891",
					new Nome("Eder", "Reis"),
					fakeVeiculoRepositorio._veiculos.First(),
					2,
					DateTime.Now
					));
		}

		public IEnumerable<Condutor?> BuscarCondutorPorPlaca(Placa placa)
			=> _condutores.FindAll(x => x.Veiculos.Where(y => y.Placa.Registro == placa.Registro).Count() > 0);

		public Condutor? BuscarCondutorPorCpf(CPF cpf) => _condutores.FirstOrDefault(x => x.Cpf.Numero == cpf.Numero);

		public void SalvarCondutor(Condutor condutor)
		{
			int indice = _condutores.FindIndex(x => x.Id == condutor.Id);

			if (indice == -1)
			{
				_condutores.Add(condutor);

				return;
			}

			_condutores.Insert(indice, condutor);
		}

		public void Salvar()
		{
			throw new NotImplementedException();
		}
	}
}
