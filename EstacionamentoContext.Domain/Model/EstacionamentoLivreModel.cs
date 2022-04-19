using System.Text.RegularExpressions;

namespace EstacionamentoContext.Domain.Model
{
	public class EstacionamentoLivreModel
	{
		public sbyte DiaDaSemana { get; set; }

		public string HoraInicial { get; set; }

		public string HoraFinal { get; set; }

		public bool IsValid()
		{
			var expressaoRegularHora = new Regex("^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$");

			return expressaoRegularHora.IsMatch(HoraInicial) && expressaoRegularHora.IsMatch(HoraFinal) && DiaDaSemana >= 0 && DiaDaSemana <= 6;
		}
	}
}
