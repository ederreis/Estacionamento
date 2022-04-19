using Estacionamento.Infra.Contexto;
using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;

namespace Estacionamento.Infra.Repositorios.Parametrizacoes
{
	public class EstacionamentoLivreRepositorio :
		Repositorio<EstacionamentoLivre>,
		IEstacionamentoLivreRepositorio
	{
		public EstacionamentoLivreRepositorio(EstacionamentoContexto contexto) : base(contexto)
		{
		}

		public IReadOnlyCollection<EstacionamentoLivre> ListarEstacionamentoLivre()
		{
			var lista = _contexto.EstacionamentoLivre;

			if (lista == null || lista.Count() == 0)
				return new List<EstacionamentoLivre>()
				{
					new EstacionamentoLivre(DayOfWeek.Monday,
						new ControleVigencia(DateTime.Parse("11:30"), DateTime.Parse("13:00"))),
					new EstacionamentoLivre(DayOfWeek.Wednesday,
						new ControleVigencia(DateTime.Parse("11:30"), DateTime.Parse("13:00"))),
					new EstacionamentoLivre(DayOfWeek.Thursday,
						new ControleVigencia(DateTime.Parse("11:30"), DateTime.Parse("13:00")))
				};

			return lista.AsEnumerable().ToArray();
		}

		public void SalvarEstacionamentoLivre(EstacionamentoLivre estacionamentoLivre)
		{
			if (estacionamentoLivre.Persistido)
				_contexto.EstacionamentoLivre.Update(estacionamentoLivre);
			else
				_contexto.EstacionamentoLivre.Add(estacionamentoLivre);
		}
	}
}
