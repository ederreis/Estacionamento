using EstacionamentoContext.Domain.Entidades.Cliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Infra.Mapeamentos.Cliente
{
	public class CondutorMap : IEntityTypeConfiguration<Condutor>
	{
		public void Configure(EntityTypeBuilder<Condutor> builder)
		{
			builder.ToTable("CONDUTOR");

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn();

			builder.OwnsOne(x => x.Cpf, cpf => 
			{
				cpf.Property(x => x.Numero)
					.HasColumnName("CPF")
					.HasColumnType("NVARCHAR(11)")
					.IsRequired();
			});

			builder.OwnsOne(x => x.Nome, nome => 
			{
				nome.Property(x => x.PrimeiroNome)
					.HasColumnName("PRIMEIRO_NOME")
					.HasColumnType("NVARCHAR(30)")
					.IsRequired();
				nome.Property(x => x.SobreNome)
					.HasColumnName("SOBRE_NOME")
					.HasColumnType("NVARCHAR(30)")
					.IsRequired();
			});

			builder.Property(x => x.QtdTicket50PorCentoDesconto)
					.HasColumnName("QTD_TICKET_DESCONTO")
					.HasColumnType("SMALLINT");

			builder.Property(x => x.CadastradoEm)
				.HasColumnName("CADASTRADO_EM")
				.HasColumnType("DATETIME")
				.HasDefaultValue(DateTime.Now)
				.IsRequired();
		}
	}
}
