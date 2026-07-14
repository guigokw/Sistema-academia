using Microsoft.EntityFrameworkCore;

public interface ITreinoAlunoRepository
{
    Treino? acharTreino(int id);

    Aluno? acharAluno(string cpf);

    TreinoAluno? acharTreinoAluno(int id, string cpf);

    void addTreinoAluno(TreinoAluno treinoAluno);

    void removeTreinoAluno(TreinoAluno treinoAluno);

    Task<List<TreinoAluno>> consultarTreinosAlunoEspecifico(string cpf);
}

public class TreinoAlunoRepository : ITreinoAlunoRepository
{
    private readonly AppDataContext _context;

    public TreinoAlunoRepository(AppDataContext context)
    {
        _context = context;
    }

    public Treino? acharTreino(int id)
    {
        return _context.Treinos.FirstOrDefault(a => a.idTreino == id);
    }

    public Aluno? acharAluno(string cpf)
    {
        return _context.Alunos.FirstOrDefault(a => a.CpfAluno == cpf);
    }

    public TreinoAluno? acharTreinoAluno(int id, string cpf)
    {
        return _context.TreinoAluno.FirstOrDefault(a => a.Treino.idTreino == id && a.Aluno.CpfAluno == cpf);
    }

    public void addTreinoAluno(TreinoAluno treinoAluno)
    {
        _context.TreinoAluno.Add(treinoAluno);
        _context.SaveChanges();
    }

    public void removeTreinoAluno(TreinoAluno treinoAluno)
    {
        _context.TreinoAluno.Remove(treinoAluno);
        _context.SaveChanges();

    }

    public async Task<List<TreinoAluno>> consultarTreinosAlunoEspecifico(string cpf)
    {
        return await _context.TreinoAluno
         .Include(t => t.Aluno)
         .Include(t => t.Treino)
         .Where(t => t.CpfAluno == cpf)
         .ToListAsync();
    }
}