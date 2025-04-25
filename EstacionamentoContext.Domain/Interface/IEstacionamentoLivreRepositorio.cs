using EstacionamentoContext.Domain.Entidades.Parametrizacoes;

namespace EstacionamentoContext.Domain.Interface;

public interface IEstacionamentoLivreRepositorio : IRepositorio<EstacionamentoLivre>
{
	IReadOnlyCollection<EstacionamentoLivre> ListarEstacionamentoLivre();

	void SalvarEstacionamentoLivre(EstacionamentoLivre estacionamentoLivre);
}
