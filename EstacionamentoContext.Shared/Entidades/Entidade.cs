using EstacionamentoContext.Shared.Notificaveis;

namespace EstacionamentoContext.Shared.Entidades;

public abstract class Entidade : Notificavel
{
	public Entidade()
	{
		Id = 0;

		CadastradoEm = DateTime.Now;
	}

#if DEBUG
	protected Entidade(DateTime cadastradoEm) : this() => CadastradoEm = cadastradoEm;
#endif

	public int Id { get; private set; }

	public DateTime CadastradoEm { get; private set; }

	public virtual bool Persistido { get { return Id != 0; } }
}
