using Estacionamento.Infra.Contexto;
using EstacionamentoContext.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infra.Repositorios
{
	public class Repositorio<T> : IRepositorio<T> where T : class
	{
		internal EstacionamentoContexto _contexto;

		internal DbSet<T> dbSet;

		public Repositorio(EstacionamentoContexto contexto)
		{
			_contexto = contexto;

			dbSet = contexto.Set<T>();
		}

		public void Salvar()
		{
			try
			{
				_contexto.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}
	}

}
