using EstacionamentoContext.Domain.Entidades.Parametrizacoes;

namespace EstacionamentoContext.Domain.Interface
{
	public interface IToleranciaRepositorio : IRepositorio<Tolerancia>
	{
		Tolerancia BuscarTolerancia();
	}
}
