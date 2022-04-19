using EstacionamentoContext.Domain.Entidades.Parametrizacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Infra.Mapeamentos.Parametrizacoes
{
	public class PrecoMap : IEntityTypeConfiguration<Preco>
	{
		public void Configure(EntityTypeBuilder<Preco> builder)
		{
			builder.ToTable("PRECO");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn();

			builder.OwnsOne(x => x.ValorHora, valorHora => 
			{
				valorHora.Property(x => x.Inicial)
					.HasColumnName("VALOR_HORA_INICIAL")
					.HasColumnType("MONEY")
					.IsRequired();
				valorHora.Property(x => x.Adicional)
					.HasColumnName("VALOR_HORA_ADICIONAL")
					.HasColumnType("MONEY")
					.IsRequired();

				valorHora.Ignore(x => x.Notifications);
			});

			builder.OwnsOne(x => x.ControleVigencia, controle => 
			{
				controle.Property(x => x.Inicio)
					.HasColumnName("VIGENCIA_INICIO")
					.HasColumnType("DATETIME")
					.IsRequired();
				controle.Property(x => x.Final)
					.HasColumnName("VIGENCIA_FINAL")
					.HasColumnType("DATETIME")
					.IsRequired();

				controle.Ignore(x => x.Notifications);
			});

			builder.Property(x => x.CadastradoEm)
				.HasColumnName("CADASTRADO_EM")
				.HasColumnType("DATETIME")
				.HasDefaultValue(DateTime.Now)
				.IsRequired();
		}
	}
}
