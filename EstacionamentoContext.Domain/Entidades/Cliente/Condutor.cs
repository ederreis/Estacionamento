using EstacionamentoContext.Domain.Entidades.Bem;
using EstacionamentoContext.Domain.ObjetoValores;
using EstacionamentoContext.Shared.Entidades;

namespace EstacionamentoContext.Domain.Entidades.Cliente;

public class Condutor : Entidade
{
	private Condutor() => _veiculos = new List<Veiculo>();

#if DEBUG
	public Condutor(
		CPF cpf,
		Nome nome,
		Veiculo veiculo,
		sbyte qtdTicket50PorCentoDesconto,
		DateTime cadastradoEm) :
		base(cadastradoEm)
	{
		Cpf = cpf;

		Nome = nome;

		_veiculos = new List<Veiculo>();

		_veiculos.Add(veiculo);

		QtdTicket50PorCentoDesconto = qtdTicket50PorCentoDesconto;
	}
#endif

	public Condutor(CPF cpf, Nome nome, Veiculo veiculo) : base()
	{
		Cpf = cpf;

		Nome = nome;

		_veiculos = new List<Veiculo>();

		_veiculos.Add(veiculo);

		QtdTicket50PorCentoDesconto = 2;
	}

	public CPF Cpf { get; private set; } = null!;

	public Nome Nome { get; private set; } = null!;

	public sbyte? QtdTicket50PorCentoDesconto { get; private set; } = null;

	public bool AptoDesconto => QtdTicket50PorCentoDesconto.HasValue && QtdTicket50PorCentoDesconto.Value > 0;

	public void ConsumirDesconto()
	{
		if (AptoDesconto)
			QtdTicket50PorCentoDesconto--;
	}

	private List<Veiculo> _veiculos { get; set; }

	public IReadOnlyCollection<Veiculo> Veiculos
	{
		get { return _veiculos.ToArray(); }
	}
}
