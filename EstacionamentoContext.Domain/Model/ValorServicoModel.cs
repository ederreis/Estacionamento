namespace EstacionamentoContext.Domain.Model;

public class ValorServicoModel
{
	public ValorServicoModel(string registroPlaca, decimal total, long ticks, decimal valorHoraInicial, decimal valorHoraAdicional, long totalDeHorasEstacionadasTicks)
	{
		RegistroPlaca = registroPlaca;

		TotalAPagar = total;

		Ticks = ticks;

		ValorHoraAdicional = valorHoraAdicional;

		ValorHoraInicial = valorHoraInicial;

		_totalDeHorasEstacionadasTicks = totalDeHorasEstacionadasTicks;
	}

	public string RegistroPlaca { get; private set; }

	public decimal ValorHoraInicial { get; private set; }

	public decimal ValorHoraAdicional { get; private set; }

	public decimal TotalAPagar { get; private set; }

	public long Ticks { get; private set; }

	private long _totalDeHorasEstacionadasTicks { get; set; }

	public string TotalDeHorasEstacionadas
	{
		get
		{
			return ConverterEmMensagem(TimeSpan.FromTicks(_totalDeHorasEstacionadasTicks));
		}
	}

	public string TotalSemEstacionamentoLivre
	{
		get
		{
			return ConverterEmMensagem(TimeSpan.FromTicks(Ticks));
		}
	}

	private string ConverterEmMensagem(TimeSpan tempoDecorrido) => string.Format("Dias: {0}, Horas: {1}, Minutos: {2}, Segundos: {3}", tempoDecorrido.Days, tempoDecorrido.Hours, tempoDecorrido.Minutes, tempoDecorrido.Seconds);

	public bool AptoParaParaCadastrarCondutor { get { return Ticks >= 360000000000; } }
}
