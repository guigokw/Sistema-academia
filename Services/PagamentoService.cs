public interface IPagamentoService
{
    Pagamento obterPagamento(int id);

    Matricula obterMatricula(int id);

    void realizarPagamento(int idPagamento, Matricula matricula);

    void cancelarPagamento(int idPagamento);

    Task<decimal?> exibirFaturamentoMensal(int mes, int ano);


} 

public class PagamentoService : IPagamentoService
{

    private readonly IPagamentoRepository _pagamentoRepository;

    public PagamentoService(IPagamentoRepository pagamentoRepository)
    {
        _pagamentoRepository = pagamentoRepository;
    }
    public Pagamento obterPagamento(int id)
    {
        Pagamento? pagamento = _pagamentoRepository.acharPagamento(id);

        if (pagamento == null)
        {
            throw new PagamentoInvalidoException("não foi possível fazer essa operação pois o pagamento não foi encontrado");
        }

        return pagamento;
    }

    public Matricula obterMatricula(int id)
    {
        Matricula? matricula = _pagamentoRepository.acharMatricula(id);

        if (matricula == null)
        {
            throw new MatriculaNaoEncontradaException("não foi possível concluir essa operação pois a matrícula não foi encontrada");
        }

        return matricula;
    }

    public void realizarPagamento(int idPagamento, Matricula matricula)
{
    Pagamento pagamento = new Pagamento()
    {
        IdPagamento = idPagamento,

        IdMatricula = matricula.idMatricula,

        ValorPagamento = CalcularValorMensal(matricula.Plano),

        DataPagamento = DateOnly.FromDateTime(DateTime.Today),

        MesReferencia = DateTime.Today.Month,

        AnoReferencia = DateTime.Today.Year
    };

    var pagamentoExiste = _pagamentoRepository.acharPagamento(pagamento.IdPagamento);

    if (pagamentoExiste != null)
    {
        throw new PagamentoInvalidoException("não foi possível realizar o pagamento pois seu identificador único está repetido com outr pagamento");
    }

    _pagamentoRepository.addPagamento(pagamento);
}

    public void cancelarPagamento(int idPagamento)
    {
        
        Pagamento pagamento = obterPagamento(idPagamento);
        _pagamentoRepository.removePagamento(pagamento);
        
    }

    private decimal CalcularValorMensal(Plano plano)
{
    return plano.TipoPlano switch
    {
        TiposDePlano.Mensal => plano.ValorPlano,

        TiposDePlano.Trimestral => plano.ValorPlano / 3m,

        TiposDePlano.Semestral => plano.ValorPlano / 6m,

        TiposDePlano.Anual => plano.ValorPlano / 12m,

        _ => throw new ArgumentException("Plano inválido")
    };
}

public async Task<decimal?> exibirFaturamentoMensal(int mes, int ano)
    {
        var pagamentoMensal = await _pagamentoRepository.calcularFaturamentoMensal(mes, ano);

        if (pagamentoMensal <= 0)
        {
            throw new PagamentoInvalidoException("não foi possível listar os pagamentos desse periodo pois não há nenhum pagamento registrado durante esse tempo");
        }

        return pagamentoMensal;
    }


}