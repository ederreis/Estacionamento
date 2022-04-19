using EstacionamentoContext.Shared.Comandos;

namespace EstacionamentoContext.Shared.Handlers
{
	public interface IHandler<T> where T : IComando
	{
		IResultadoComando Handle(T comando);
	}
}
