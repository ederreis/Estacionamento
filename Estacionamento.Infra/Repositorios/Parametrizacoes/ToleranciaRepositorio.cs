using Estacionamento.Infra.Contexto;
using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;

namespace Estacionamento.Infra.Repositorios.Parametrizacoes
{
	public class ToleranciaRepositorio : Repositorio<Tolerancia>, IToleranciaRepositorio
	{
		public ToleranciaRepositorio(EstacionamentoContexto contexto) : base(contexto)
		{
		}

		public Tolerancia BuscarTolerancia()
			=> new Tolerancia();
	}
}
