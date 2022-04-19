using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.Entidades.Cliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Infra.Mapeamentos.Bem
{
	public class VeiculoMap : IEntityTypeConfiguration<Veiculo>
	{
		public void Configure(EntityTypeBuilder<Veiculo> builder)
		{
			builder.ToTable("VEICULO");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn();

			builder.OwnsOne(x => x.Placa, placa =>
			{
				placa.Property(x => x.Registro)
					.HasColumnName("REGISTRO")
					.HasColumnType("NVARCHAR(7)")
					.IsRequired();

				placa.Ignore(x => x.Notifications);
			});

			builder.HasMany(x => x.Condutores)
				.WithMany(x => x.Veiculos)
				.UsingEntity<Dictionary<string, object>>(
					"VeiculoCondutor",
						veiculo => veiculo.HasOne<Condutor>()
							.WithMany()
							.HasForeignKey("CONDUTORID")
							.HasConstraintName("FK_VEICULOCONDUTOR_CONDUTORID")
							.OnDelete(DeleteBehavior.Restrict),
						condutores => condutores.HasOne<Veiculo>()
							.WithMany()
							.HasForeignKey("VEICULOID")
							.HasConstraintName("FK_VEICULOCONDUTOR_VEICULOID")
							.OnDelete(DeleteBehavior.Restrict));

			builder.Property(x => x.CadastradoEm)
				.HasColumnName("CADASTRADO_EM")
				.HasColumnType("DATETIME")
				.HasDefaultValue(DateTime.Now)
				.IsRequired();
		}
	}
}
