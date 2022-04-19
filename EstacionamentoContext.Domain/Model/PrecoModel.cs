namespace EstacionamentoContext.Domain.Model
{
	public class PrecoModel
	{
		public decimal ValorHoraInicial { get; set; }

		public decimal ValorHoraAdicional { get; set; }

		public string DataInicioVigencia { get; set; }

		public string DataFinalVigencia { get; set; }
	}
}
