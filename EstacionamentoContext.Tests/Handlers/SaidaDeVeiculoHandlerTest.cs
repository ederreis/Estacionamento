using EstacionamentoContext.Domain.Comandos;
using EstacionamentoContext.Domain.Handlers;
using EstacionamentoContext.Tests.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EstacionamentoContext.Tests.Handlers
{
	[TestClass]
	public class SaidaDeVeiculoHandlerTest
	{
		[TestMethod]
		public void DeveRetornarErroCarroCadastradoSemContrato()
		{
			var comando = new RegistrarSaidaVeiculoComando() { RegistroPlaca = "abc1113" };

			var fakeVeiculoRepositorio = new FakeVeiculoRepositorio();

			var handler = new SaidaDeVeiculoHandler(new FakeCondutorRepositorio(fakeVeiculoRepositorio), new FakeAgregadoRepositorio(fakeVeiculoRepositorio), fakeVeiculoRepositorio);

			handler.Handle(comando);

			Assert.IsFalse(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarErroCarroCadastradoContratoFinalizado()
		{
			var comando = new RegistrarSaidaVeiculoComando() { RegistroPlaca = "abc1111" };

			var fakeVeiculoRepositorio = new FakeVeiculoRepositorio();

			var fakeCondutorRepositorio = new FakeCondutorRepositorio(fakeVeiculoRepositorio);

			var fakeEventoRepositorio = new FakeAgregadoRepositorio(fakeVeiculoRepositorio);

			var handler1 = new SaidaDeVeiculoHandler(fakeCondutorRepositorio, fakeEventoRepositorio, fakeVeiculoRepositorio);

			handler1.Handle(comando);

			comando = new RegistrarSaidaVeiculoComando() { RegistroPlaca = "abc1111" };

			var handler = new SaidaDeVeiculoHandler(fakeCondutorRepositorio, fakeEventoRepositorio, fakeVeiculoRepositorio);

			handler.Handle(comando);

			Assert.IsFalse(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarSucessoCarroCadastradoContratoEmAberto()
		{
			var comando = new RegistrarSaidaVeiculoComando() { RegistroPlaca = "abc1111" };

			var fakeVeiculoRepositorio = new FakeVeiculoRepositorio();

			var fakeCondutorRepositorio = new FakeCondutorRepositorio(fakeVeiculoRepositorio);

			var fakeEventoRepositorio = new FakeAgregadoRepositorio(fakeVeiculoRepositorio);

			var handler = new SaidaDeVeiculoHandler(fakeCondutorRepositorio, fakeEventoRepositorio, fakeVeiculoRepositorio);

			handler.Handle(comando);

			Assert.IsTrue(handler.IsValid);
		}

		[TestMethod]
		public void DeveRetornarSucessoCadastrarCondutorNoPrazo()
		{
			var comando = new RegistrarNovoCondutorCommand()
			{
				RegistroPlaca = "abc1111",
				Cpf = "01234567891",
				PrimeiroNome = "Chico",
				SobreNome = "Cascao",
				Ticks = 1000000000000
			};

			var fakeVeiculoRepositorio = new FakeVeiculoRepositorio();

			var fakeCondutorRepositorio = new FakeCondutorRepositorio(fakeVeiculoRepositorio);

			var fakeEventoRepositorio = new FakeAgregadoRepositorio(fakeVeiculoRepositorio);

			var handler = new SaidaDeVeiculoHandler(fakeCondutorRepositorio, fakeEventoRepositorio, fakeVeiculoRepositorio);

			handler.Handle(comando);

			Assert.IsTrue(handler.IsValid);
		}
	}
}
