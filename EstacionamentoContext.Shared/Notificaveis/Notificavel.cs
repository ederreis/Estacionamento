namespace EstacionamentoContext.Shared.Notificaveis
{
	public abstract class Notificavel
	{
		private bool _isValid = true;

		public virtual bool IsValid
		{
			get { return _isValid; }
		}

		private List<KeyValuePair<string, string>> _itens = new List<KeyValuePair<string, string>>();

		public virtual void AddNotification(string key, string mensagem)
		{
			_isValid = false;

			_itens.Add(new KeyValuePair<string, string>(key, mensagem));
		}

		public virtual void AddNotifications(List<KeyValuePair<string, string>> notifications)
		{
			if (notifications.Count > 0)
				_isValid = false;

			_itens.AddRange(notifications);
		}

		public virtual List<KeyValuePair<string, string>> Notifications
		{
			get { return _itens; }
		}
	}
}
