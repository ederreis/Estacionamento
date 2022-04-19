using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Domain.Queries;

namespace EstacionamentoContext.Tests.Fake
{
	public class FakePrecoRepositorio : IPrecoRepositorio
	{
		List<Preco> _precos { get; set; } = new List<Preco>();

		public FakePrecoRepositorio()
		{
			var valorHora = new ValorHora(1, 1);

			var controleVigencia = new ControleVigencia(DateTime.Parse("01/01/2022"), DateTime.Parse("31/12/2022"));

			var preco = new Preco(valorHora, controleVigencia);

			_precos.Add(preco);
		}

		public Preco? BuscarPrecoParametrizadoPorData(DateTime entrada)
			=> _precos.AsQueryable().FirstOrDefault(PrecoQueries.BuscarPorData(entrada));		

		public void SalvarParametrizacao(Preco preco)
		{
			int indice = _precos.FindIndex(x => x.Id == preco.Id);

			if (indice == -1)
			{
				_precos.Add(preco);

				return;
			}

			_precos.Insert(indice, preco);
		}

		public void Salvar()
		{
		
		}
	}
}
