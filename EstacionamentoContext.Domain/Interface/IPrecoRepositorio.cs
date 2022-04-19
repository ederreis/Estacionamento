using EstacionamentoContext.Domain.Entidades.Parametrizacoes;

namespace EstacionamentoContext.Domain.Interface
{
	public interface IPrecoRepositorio :IRepositorio<Preco>
	{
		Preco? BuscarPrecoParametrizadoPorData(DateTime entrada);

		void SalvarParametrizacao(Preco preco);
	}
}
