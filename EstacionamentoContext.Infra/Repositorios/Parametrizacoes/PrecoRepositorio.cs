using EstacionamentoContext.Infra.Contexto;
using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.Queries;

namespace EstacionamentoContext.Infra.Repositorios.Parametrizacoes;

public class PrecoRepositorio : Repositorio<Preco>, IPrecoRepositorio
{
	public PrecoRepositorio(EstacionamentoContexto contexto) : base(contexto)
	{
	}

	public Preco? BuscarPrecoParametrizadoPorData(DateTime entrada)
		=> _contexto.Precos.AsQueryable().FirstOrDefault(PrecoQueries.BuscarPorData(entrada));

	public void SalvarParametrizacao(Preco preco)
	{
		if (preco.Persistido)
			_contexto.Precos.Update(preco);
		else
			_contexto.Precos.Add(preco);
	}
}
