using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Handlers;
using EstacionamentoContext.Tests.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EstacionamentoContext.Tests.Handlers
{
	[TestClass]
	public class EntradaDeVeiculoHandlerTest
	{
		[TestMethod] 
		public void DeveRetornarErroQuandoVeiculoForInvalido()
		{
			var comando = new RegistrarEntradaVeiculoComando() { RegistroPlaca = "ABC11" };

			var fakeVeiculoRepositorio = new FakeVeiculoRepositorio();

			var fakePrecoRepositorio = new FakePrecoRepositorio();

			var handler = new EntradaDeVeiculoHandler(fakeVeiculoRepositorio, new FakeAgregadoRepositorio(fakeVeiculoRepositorio), fakePrecoRepositorio);
			handler.Handle(comando);

			Assert.IsFalse(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarSucessoQuandoVeiculoForValido()
		{
			var comando = new RegistrarEntradaVeiculoComando() { RegistroPlaca = "ABC1112" };

			var fakeVeiculoRepositorio = new FakeVeiculoRepositorio();

			var fakePrecoRepositorio = new FakePrecoRepositorio();

			var handler = new EntradaDeVeiculoHandler(fakeVeiculoRepositorio, new FakeAgregadoRepositorio(fakeVeiculoRepositorio), fakePrecoRepositorio);

			handler.Handle(comando);

			Assert.IsTrue(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarErroQuandoJaTemContrato()
		{
			var comando = new RegistrarEntradaVeiculoComando() { RegistroPlaca = "ABC1111" };

			var fakeVeiculoRepositorio = new FakeVeiculoRepositorio();

			var fakePrecoRepositorio = new FakePrecoRepositorio();

			var handler = new EntradaDeVeiculoHandler(fakeVeiculoRepositorio, new FakeAgregadoRepositorio(fakeVeiculoRepositorio), fakePrecoRepositorio);

			handler.Handle(comando);

			Assert.IsFalse(handler.IsValid);
		}
	}
}
