using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Infra.Mapeamentos.Parametrizacoes;

public class EstacionamentoLivreMap : IEntityTypeConfiguration<EstacionamentoLivre>
{
	public void Configure(EntityTypeBuilder<EstacionamentoLivre> builder)
	{
		builder.ToTable("ESTACIONAMENTO_LIVRE");

		builder.HasKey(x => (sbyte)x.DiaDaSemana);

		builder.Property(x => (sbyte)x.DiaDaSemana)
			.ValueGeneratedNever();

		builder.OwnsOne(x => x.ControleVigencia, controleVigencia =>
		{
			controleVigencia.Property(x => x.Inicio)
				.HasColumnName("VIGENCIA_INICIO")
				.HasColumnType("DATETIME")
				.IsRequired();
			controleVigencia.Property(x => x.Final)
				.HasColumnName("VIGENCIA_FINAL")
				.HasColumnType("DATETIME")
				.IsRequired();
		});
	}
}
