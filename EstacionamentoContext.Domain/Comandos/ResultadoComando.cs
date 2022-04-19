using EstacionamentoContext.Shared.Comandos;

namespace EstacionamentoContext.Domain.Comandos
{
	public sealed class ResultadoComando : IResultadoComando
	{
		public ResultadoComando(bool sucesso, string[] mensagem)
		{
			Sucesso = sucesso;

			Mensagem = mensagem;
		}

		public ResultadoComando(bool sucesso, string mensagem) : this(sucesso, new string[] {mensagem}) { }

		public bool Sucesso{get; private set;}

		public string[] Mensagem { get; private set;}
	}
}
