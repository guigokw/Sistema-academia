using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public interface IPagamentoRepository
{
    Pagamento? acharPagamento(int id);

    Matricula? acharMatricula(int id);

    void addPagamento(Pagamento pagamento);

    void removePagamento(Pagamento pagamento);

    Task<bool> ExistePagamentoDaMatricula(int idMatricula);

    Task<bool> ExistePagamentoMes(int idMatricula, int mes, int ano);

    Task<decimal?> calcularFaturamentoMensal(int mes, int ano);


}

public class PagamentoRepository : IPagamentoRepository
{

    private readonly AppDataContext _context;

    public PagamentoRepository(AppDataContext context)
    {
        _context = context;
    }
    public Pagamento? acharPagamento(int id)
    {
        return _context.Pagamentos.FirstOrDefault(a => a.IdPagamento == id);
    }

    public Matricula? acharMatricula(int id)
    {
        return _context.Matriculas.FirstOrDefault(a => a.idMatricula == id);
    }

    public void addPagamento(Pagamento pagamento)
    {
        _context.Pagamentos.Add(pagamento);
        _context.SaveChanges();
    }

    public void removePagamento(Pagamento pagamento)
    {
        _context.Pagamentos.Remove(pagamento);
        _context.SaveChanges();
    }

    public async Task<bool> ExistePagamentoDaMatricula(int idMatricula)
{
    return await _context.Pagamentos
        .AnyAsync(p => p.IdMatricula == idMatricula);
}

public async Task<bool> ExistePagamentoMes(int idMatricula, int mes, int ano)
{
    return await _context.Pagamentos.AnyAsync(p =>
        p.IdMatricula == idMatricula &&
        p.MesReferencia == mes &&
        p.AnoReferencia == ano);
}

public async Task<decimal?> calcularFaturamentoMensal(int mes, int ano)
    {
        return await _context.Pagamentos
            .Where(p => p.DataPagamento.HasValue &&
                   p.DataPagamento.Value.Month == mes &&
                   p.DataPagamento.Value.Year == ano)
            .SumAsync(m => m.ValorPagamento);
    }

}