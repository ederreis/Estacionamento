using EstacionamentoContext.Domain.Entidades.Cliente;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Entidades;

namespace EstacionamentoContext.Domain.Entidades.Bem
{
	public class Veiculo : Entidade
	{
		private Veiculo()
		{
			_condutores = new List<Condutor>();
		}

		public Veiculo(Placa placa) : base()
		{
			Placa = placa;

			_condutores = new List<Condutor>();
		}

		public Placa Placa { get; private set; } = null!;

		private List<Condutor> _condutores { get; set; }

		public IReadOnlyCollection<Condutor> Condutores 
		{ 
			get { return _condutores.ToArray(); }
		}

		public void AdicionarCondutor(Condutor condutor)
		{
			if (condutor == null)
			{
				AddNotification(nameof(Condutor), $"Não é possível vincular o condutor ao veículo { Placa.Registro }, pois o condutor é um objeto nulo.");

				return;
			}

			var indiceVetor = _condutores.FindIndex(x => x.Id == condutor.Id);

			if (indiceVetor == -1)
				_condutores.Add(condutor);
		}
	}
}
