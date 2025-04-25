using EstacionamentoContext.Domain.Agregados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstacionamentoContext.Infra.Mapeamentos.Agregados;

public class AluguelVagaAgregadoMap : IEntityTypeConfiguration<AluguelVagaAgregado>
{
	public void Configure(EntityTypeBuilder<AluguelVagaAgregado> builder)
	{
		builder.ToTable("ALUGUEL_VAGA_AGREGADO");

		builder.HasKey(x => x.Id);

		builder.HasOne(x => x.Veiculo)
			.WithMany()
			.HasConstraintName("FK_ALUGUEL_VAGA_VEICULO_ID")
			.IsRequired();

		builder.HasOne(x => x.Condutor)
			.WithMany()
			.HasConstraintName("FK_ALUGUEL_VAGA_CONDUTOR_ID");

		builder.Property(x => x.CadastradoEm)
			.HasColumnName("CADASTRADO_EM")
			.HasColumnType("DATETIME")
			.HasDefaultValue(DateTime.Now)
			.IsRequired();

		builder.Property(x => x.FinalizadoEm)
			.HasColumnName("FINALIZOU_EM")
			.HasColumnType("DATETIME");
	}
}
