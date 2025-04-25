namespace EstacionamentoContext.Domain.Interface;

public interface IRepositorio<T> where T : class
{
	void Salvar();
}