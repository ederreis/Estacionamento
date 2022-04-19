using Microsoft.Extensions.DependencyInjection;

namespace EstacionamentoContext.Domain.Configuracoes
{
	public static class ConfigurarServico
	{
		public static void AdicionarEstacionamentoController(this IMvcBuilder mvcBuilder)
			=> mvcBuilder.AddApplicationPart(typeof(ConfigurarServico).Assembly);
	}
}
