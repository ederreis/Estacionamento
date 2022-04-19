using EstacionamentoContext.Domain.Entidades.Bem;
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
			});

			builder.Property(x => x.CadastradoEm)
				.HasColumnName("CADASTRADO_EM")
				.HasColumnType("DATETIME")
				.HasDefaultValue(DateTime.Now)
				.IsRequired();
		}
	}
}
