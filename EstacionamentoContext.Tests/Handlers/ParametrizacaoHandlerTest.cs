using EstacionamentoContext.Services.Comandos;
using EstacionamentoContext.Services.Handlers;
using EstacionamentoContext.Tests.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EstacionamentoContext.Tests.Handlers
{
	[TestClass]
	public class ParametrizacaoHandlerTest
	{
		[TestMethod]
		public void DeveRetornarErroPrecoErrado()
		{
			var comando = new RegistrarPrecoComando(-1, -1, DateTime.Now, DateTime.Now.AddDays(365));

			var fakePrecoRepositorio = new FakePrecoRepositorio();

			var handler = new ParamPrecoHandler(fakePrecoRepositorio, new FakeAgregadoRepositorio(new FakeVeiculoRepositorio()));

			handler.Handle(comando);

			Assert.IsFalse(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarErroDatasInvalidasOuErradas()
		{
			var comando = new RegistrarPrecoComando(0, 1, DateTime.Now.AddDays(365), DateTime.Now);

			var fakePrecoRepositorio = new FakePrecoRepositorio();

			var handler = new ParamPrecoHandler(fakePrecoRepositorio, new FakeAgregadoRepositorio(new FakeVeiculoRepositorio()));

			handler.Handle(comando);

			Assert.IsFalse(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarSucessoQuandoDataEstiverCorreta()
		{
			var comando = new RegistrarPrecoComando(0, 1, DateTime.Now.AddDays(366), DateTime.Now.AddDays(731));

			var fakePrecoRepositorio = new FakePrecoRepositorio();

			var handler = new ParamPrecoHandler(fakePrecoRepositorio, new FakeAgregadoRepositorio(new FakeVeiculoRepositorio()));

			handler.Handle(comando);

			Assert.IsTrue(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarErroQuandoDiaDaSemanaErrado()
		{
			var comando = new RegistrarEstacionamentoLivreComando()
			{
				DiaDaSemana = 8,
				Inicio = DateTime.Parse("8:00"),
				Final = DateTime.Parse("10:30")
			};

			var repositorio = new FakeEstacionamentoLivreRepositorio();

			var handler = new ParamEstacionamentoLivreHandler(repositorio, new FakeAgregadoRepositorio(new FakeVeiculoRepositorio()));

			handler.Handle(comando);

			Assert.IsFalse(handler.IsValid);
		}

		[TestMethod]
		public void DeeRetornarErroQuandoHoraEstiverErrada()
		{
			var comando = new RegistrarEstacionamentoLivreComando()
			{
				DiaDaSemana = 2,
				Inicio = DateTime.Parse("18:00"),
				Final = DateTime.Parse("10:30")
			};

			var repositorio = new FakeEstacionamentoLivreRepositorio();

			var handler = new ParamEstacionamentoLivreHandler(repositorio, new FakeAgregadoRepositorio(new FakeVeiculoRepositorio()));

			handler.Handle(comando);

			Assert.IsFalse(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarVerdeiroParametrizacaoEstacionamentoLivre()
		{
			var comando = new RegistrarEstacionamentoLivreComando()
			{
				DiaDaSemana = 3,
				Inicio = DateTime.Parse("8:00"),
				Final = DateTime.Parse("10:30")
			};

			var repositorio = new FakeEstacionamentoLivreRepositorio();

			var handler = new ParamEstacionamentoLivreHandler(repositorio, new FakeAgregadoRepositorio(new FakeVeiculoRepositorio()));

			handler.Handle(comando);

			Assert.IsTrue(handler.IsValid);
		}
	}
}
