using Estacionamento.Infra.Contexto;
using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Domain.Queries;

namespace Estacionamento.Infra.Repositorios.Bem
{
	public class VeiculoRepositorio : 
		Repositorio<Veiculo>, 
		IVeiculoRepositorio
	{
		public VeiculoRepositorio(EstacionamentoContexto contexto) : base(contexto)
		{
		}

		public Veiculo? BuscarVeiculoPorPlaca(Placa placa)
		=> _contexto.Veiculos
			.AsQueryable()
			.FirstOrDefault(VeiculoQueries.BucaPorPlaca(placa));

		public void SalvarVeiculo(Veiculo veiculo)
		{
			if (veiculo.Id == 0)
				_contexto.Veiculos.Add(veiculo);
			else
				_contexto.Veiculos.Update(veiculo);
		}

	}
}
