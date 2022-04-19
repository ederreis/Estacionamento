using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.ObjetoValores;
using System.Linq.Expressions;

namespace EstacionamentoContext.Domain.Queries
{
	public static class VeiculoQueries
	{
		public static Expression<Func<Veiculo, bool>> BucaPorPlaca(Placa placa)
			=> x => x.Placa.Registro == placa.Registro;
	}
}
