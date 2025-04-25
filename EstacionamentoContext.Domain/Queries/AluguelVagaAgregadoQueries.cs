using EstacionamentoContext.Domain.Agregados;
using EstacionamentoContext.Domain.ObjetoValores;
using System.Linq.Expressions;

namespace EstacionamentoContext.Domain.Queries;

public static class AluguelVagaAgregadoQueries
{
	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarContratoNaoFinalizado(Placa placa)
		=> x => x.Veiculo.Placa.Registro == placa.Registro && !x.FinalizadoEm.HasValue;

	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarAluguelVagaPorPlacaComCondutor(Placa placa)
		=> x => x.Veiculo.Placa.Registro == placa.Registro && x.Condutor != null;

	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarHistorico(CPF cpf)
		=> x => x.Condutor != null && x.Condutor.Cpf.Numero == cpf.Numero;

	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarHistorico(Placa placa)
		=> x => x.Veiculo.Placa.Registro == placa.Registro;

	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarContratosFinalizados()
		=> x => x.FinalizadoEm.HasValue;

	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarContratosFinalizadosNoDia(DateTime data)
		=> x => x.FinalizadoEm.HasValue && x.FinalizadoEm.Value.Date == data.Date;

	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarContratosFinalizadosAnteriormente(DateTime data)
		=> x => x.FinalizadoEm.HasValue && x.FinalizadoEm.Value.Date < data.Date;

	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarContratosEmAberto()
		=> x => !x.FinalizadoEm.HasValue;

	public static Expression<Func<AluguelVagaAgregado, bool>> BuscarRegistroEmAberto(DateTime inicio, DateTime final)
		=> x => (x.CadastradoEm >= inicio || x.CadastradoEm <= final) && !x.FinalizadoEm.HasValue;
}
