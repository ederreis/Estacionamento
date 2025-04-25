using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;

namespace EstacionamentoContext.Tests.Fake
{
	public class FakeEstacionamentoLivreRepositorio : IEstacionamentoLivreRepositorio
	{
		List<EstacionamentoLivre> _estacionamentoLivre { get; set; } = new List<EstacionamentoLivre>();

		public FakeEstacionamentoLivreRepositorio()
		{

		}

		public IReadOnlyCollection<EstacionamentoLivre> ListarEstacionamentoLivre()
		{
			if (_estacionamentoLivre.Any())
				return _estacionamentoLivre.ToArray();
			else
			{
				var vigenciaPadrao = new ControleVigencia(DateTime.Parse("11:30"), DateTime.Parse("13:00"));

				return new List<EstacionamentoLivre>()
				{
					new EstacionamentoLivre(DayOfWeek.Monday, vigenciaPadrao),
					new EstacionamentoLivre(DayOfWeek.Thursday, vigenciaPadrao),
					new EstacionamentoLivre(DayOfWeek.Friday, vigenciaPadrao)
				};
			}
		}

		public void SalvarEstacionamentoLivre(EstacionamentoLivre estacionamentoLivre)
		{
			int indice = _estacionamentoLivre.FindIndex(x => x.DiaDaSemana == estacionamentoLivre.DiaDaSemana);

			if (indice == -1)
			{
				_estacionamentoLivre.Add(estacionamentoLivre);

				return;
			}

			_estacionamentoLivre.Insert(indice, estacionamentoLivre);
		}

		public Tolerancia BuscarTolerancia() => new Tolerancia();

		public void Salvar()
		{

		}
	}
}
