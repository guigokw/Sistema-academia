using Microsoft.EntityFrameworkCore;

public interface IMatriculaRepository
{

    Matricula? acharMatricula(int id);
    Matricula? acharMatriculaPorAluno(string cpfAluno, int idMatricula);

    Aluno? acharAluno(string cpfAluno);

    Plano? acharPlano(int idPlano);
    void addMatricula(Matricula matricula);

    void renovaMatricula(Matricula matricula);

    void CancelMatricula(Matricula matricula);

    Task<List<Matricula>> obterMatriculasAtrasada();

    Task<List<Matricula>> obterMatriculasPagas();

    Task<List<Matricula>> ObterTodas();

    Task<List<Matricula>> obterMatriculasPendentes();

    Task<List<Matricula>> ObterMatriculasProximasDoVencimento();

    void atualizarSituacaoTodos();

}

public class MatriculaRepository : IMatriculaRepository
{

    public AppDataContext _context;

    public MatriculaRepository(AppDataContext context)
    {
        _context = context;
    }

    public Matricula? acharMatricula(int id)
    {
       return _context.Matriculas.FirstOrDefault(a => a.idMatricula == id);
    }

    public Aluno? acharAluno(string cpfAluno)
    {
        return _context.Alunos.FirstOrDefault(a => a.CpfAluno == cpfAluno);
    }

    public Plano? acharPlano(int idPlano)
    {
        return _context.Planos.FirstOrDefault(p => p.IdPlano == idPlano);
    }

    public Matricula? acharMatriculaPorAluno(string cpfAluno, int idMatricula)
    {
        return _context.Matriculas.FirstOrDefault(m => m.CpfAluno == cpfAluno && m.idMatricula == idMatricula);   
    }
    public void addMatricula(Matricula matricula)
    {
        _context.Matriculas.Add(matricula);
        _context.SaveChanges();
    }

    public void CancelMatricula(Matricula matricula)
    {
        _context.Matriculas.Remove(matricula);
        _context.SaveChanges();
        
    }

   public void renovaMatricula(Matricula matricula)
{
    switch (matricula.Plano.TipoPlano)
    {
        case TiposDePlano.Mensal:
            matricula.DataVencimento =
                matricula.DataVencimento.AddMonths(1);
            break;

        case TiposDePlano.Trimestral:
            matricula.DataVencimento =
                matricula.DataVencimento.AddMonths(3);
            break;

        case TiposDePlano.Semestral:
            matricula.DataVencimento =
                matricula.DataVencimento.AddMonths(6);
            break;

        case TiposDePlano.Anual:
            matricula.DataVencimento =
                matricula.DataVencimento.AddYears(1);
            break;
    }

    matricula.SituacaoPagamento = SituacaoPagamento.Paga;

    _context.SaveChanges();
}

    public async Task<List<Matricula>> obterMatriculasAtrasada()
{
    return await _context.Matriculas
        .Include(m => m.Aluno)
        .Include(m => m.Plano)
        .Where(m => m.SituacaoPagamento == SituacaoPagamento.Atrasada)
        .ToListAsync();
}

    public async Task<List<Matricula>> obterMatriculasPagas()
{
    return await _context.Matriculas
        .Include(m => m.Aluno)
        .Include(m => m.Plano)
        .Where(m => m.SituacaoPagamento == SituacaoPagamento.Paga)
        .ToListAsync();
}

public async Task<List<Matricula>> ObterTodas()
{
    return await _context.Matriculas
        .Include(m => m.Aluno)
        .Include(m => m.Plano)
        .ToListAsync();
}

public async Task<List<Matricula>> obterMatriculasPendentes()
{
    return await _context.Matriculas
        .Include(m => m.Aluno)
        .Include(m => m.Plano)
        .Where(m => m.SituacaoPagamento == SituacaoPagamento.Pendente)
        .ToListAsync();
}

public void atualizarSituacaoTodos()
    {
        _context.SaveChanges();
    }

public async Task<List<Matricula>> ObterMatriculasProximasDoVencimento()
{
    DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
    DateOnly limite = hoje.AddDays(7);

    return await _context.Matriculas
        .Include(m => m.Aluno)
        .Where(m => m.DataVencimento >= hoje &&
                    m.DataVencimento <= limite)
        .OrderBy(m => m.DataVencimento)
        .ToListAsync();
}

}