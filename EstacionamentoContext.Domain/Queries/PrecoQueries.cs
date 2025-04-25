using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using System.Linq.Expressions;

namespace EstacionamentoContext.Domain.Queries;

public static class PrecoQueries
{
	public static Expression<Func<Preco, bool>> BuscarPorData(DateTime entrada)
		=> x => x.ControleVigencia.Inicio <= entrada && x.ControleVigencia.Final >= entrada;
}
