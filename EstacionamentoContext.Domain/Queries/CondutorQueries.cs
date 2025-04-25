using EstacionamentoContext.Domain.Entidades.Cliente;
using EstacionamentoContext.Domain.ObjetoValores;
using System.Linq.Expressions;

namespace EstacionamentoContext.Domain.Queries;

public static class CondutorQueries
{
	public static Expression<Func<Condutor, bool>> BuscaPorCpf(CPF cpf)
		=> x => x.Cpf.Numero == cpf.Numero;
}
