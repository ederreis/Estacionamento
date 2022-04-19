using Estacionamento.Infra.Contexto;
using Estacionamento.Infra.Repositorios.Agregado;
using Estacionamento.Infra.Repositorios.Bem;
using Estacionamento.Infra.Repositorios.Cliente;
using Estacionamento.Infra.Repositorios.Parametrizacoes;
using EstacionamentoContext.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Estacionamento.Infra.Configuracoes
{
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
}
