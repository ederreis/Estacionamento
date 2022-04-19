using EstacionamentoContext.Domain.Comandos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EstacionamentoContext.Tests.Comandos
{
	[TestClass]
	public class RegistrarEntradaDeVeiculoComandoTeste
	{
		private RegistrarEntradaVeiculoComando comando;

		public RegistrarEntradaDeVeiculoComandoTeste() => comando = new RegistrarEntradaVeiculoComando();		

		[TestMethod]
		public void DeveRetornarErroQuandoPlacaRegistroEstiverErrada()
		{
			comando.RegistroPlaca = "AB11";

			comando.Validar();

			Assert.IsFalse(comando.IsValid);
		}

		[TestMethod]
		public void DeveRetornarCorretoQuandoPlacaRegistroEstiverCerta()
		{
			comando.RegistroPlaca = "ABC1111";

			comando.Validar();

			Assert.IsTrue(comando.IsValid);
		}
	}
}
