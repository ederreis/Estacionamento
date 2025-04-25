using EstacionamentoContext.Infra.Contexto;
using EstacionamentoContext.Infra.Repositorios.Agregado;
using EstacionamentoContext.Infra.Repositorios.Bem;
using EstacionamentoContext.Infra.Repositorios.Cliente;
using EstacionamentoContext.Infra.Repositorios.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EstacionamentoContext.Infra.Configuracoes;

public static class ConfigurarRepositorio
{
	public static void ConfigureEstacionamentoInfraServicos(this IServiceCollection collection)
	{
		collection.AddDbContext<EstacionamentoContexto>();

		collection.AddScoped<IAluguelVagaRepositorio, AluguelVagaRepositorio>();

		collection.AddScoped<ICondutorRepositorio, CondutorRepositorio>();

		collection.AddScoped<IEstacionamentoLivreRepositorio, EstacionamentoLivreRepositorio>();

		collection.AddScoped<IPrecoRepositorio, PrecoRepositorio>();

		collection.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
	}
}
