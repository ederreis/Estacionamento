using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Domain.Interface;

namespace EstacionamentoContext.Tests.Fake
{
	public class FakeVeiculoRepositorio : IVeiculoRepositorio
	{
		public List<Veiculo> _veiculos { get; private set; } = new List<Veiculo>();

		public FakeVeiculoRepositorio()
		{
			_veiculos.Add(new Veiculo((Placa)"abc1111"));
			_veiculos.Add(new Veiculo((Placa)"abc1113"));
		}

		public Veiculo? BuscarVeiculoPorPlaca(Placa placa) => _veiculos.FirstOrDefault(veiculo => veiculo.Placa.Registro == placa.Registro);

		public void SalvarVeiculo(Veiculo veiculo)
		{
			int indice = _veiculos.FindIndex(x => x.Id == veiculo.Id);

			if (indice == -1)
			{
				_veiculos.Add(veiculo);

				return;
			}

			_veiculos.Insert(indice, veiculo);
		}

		public void Salvar()
		{
		}
	}
}
