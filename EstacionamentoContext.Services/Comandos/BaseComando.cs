using EstacionamentoContext.Shared.Comandos;
using Flunt.Notifications;

namespace EstacionamentoContext.Services.Comandos;

public abstract class BaseComando : Notifiable<Notification>, IComando
{
	public virtual void Validar()
	{
		throw new NotImplementedException();
	}
}
