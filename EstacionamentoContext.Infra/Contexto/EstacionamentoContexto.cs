using EstacionamentoContext.Infra.Mapeamentos.Agregados;
using EstacionamentoContext.Infra.Mapeamentos.Bem;
using EstacionamentoContext.Infra.Mapeamentos.Cliente;
using EstacionamentoContext.Infra.Mapeamentos.Parametrizacoes;
using EstacionamentoContext.Domain.Agregados;
using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.Entidades.Cliente;
using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using Microsoft.EntityFrameworkCore;

namespace EstacionamentoContext.Infra.Contexto;

public class EstacionamentoContexto : DbContext
{
	public DbSet<Veiculo> Veiculos { get; set; } = null!;

	public DbSet<Condutor> Condutores { get; set; } = null!;

	public DbSet<EstacionamentoLivre> EstacionamentoLivre { get; set; } = null!;

	public DbSet<Preco> Precos { get; set; } = null!;

	public DbSet<AluguelVagaAgregado> Agregados { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer("");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new AluguelVagaAgregadoMap());
		modelBuilder.ApplyConfiguration(new VeiculoMap());
		modelBuilder.ApplyConfiguration(new CondutorMap());
		modelBuilder.ApplyConfiguration(new EstacionamentoLivreMap());
		modelBuilder.ApplyConfiguration(new PrecoMap());
	}
}