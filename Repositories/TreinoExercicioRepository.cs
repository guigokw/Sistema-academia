using Microsoft.EntityFrameworkCore;

public interface ITreinoExercicioRepository
{
    Treino? acharTreino(int id);

    Exercicio? acharExercicio(int id);

    TreinoExercicio? acharTreinoExercicio(int idTreino, int idExercicio);

    void addTreinoExercicio(TreinoExercicio treinoExercicio);

    void removeTreinoExercicio(TreinoExercicio treinoExercicio);

    Task<List<TreinoExercicio>> consultarExerciciosDoTreino(int id);
}

public class TreinoExercicioRepository : ITreinoExercicioRepository
{
    private readonly AppDataContext _context;

    public TreinoExercicioRepository(AppDataContext context)
    {
        _context = context;
    }

    public Treino? acharTreino(int id)
    {
        return _context.Treinos.FirstOrDefault(a => a.idTreino == id);
    }

    public Exercicio? acharExercicio(int id)
    {
        return _context.Exercicios.FirstOrDefault(a => a.IdExercicio == id);
    }

    public TreinoExercicio? acharTreinoExercicio(int idTreino, int idExercicio)
    {
        return _context.TreinoExercicio.FirstOrDefault(a => a.Treino.idTreino == idTreino && a.Exercicio.IdExercicio == idExercicio);
    }

    public void addTreinoExercicio(TreinoExercicio treinoExercicio)
    {
        _context.TreinoExercicio.Add(treinoExercicio);
        _context.SaveChanges();
    }

    public void removeTreinoExercicio(TreinoExercicio treinoExercicio)
    {
        _context.TreinoExercicio.Remove(treinoExercicio);
        _context.SaveChanges();
    }


    public async Task<List<TreinoExercicio>> consultarExerciciosDoTreino(int id)
    {
        return await _context.TreinoExercicio
        .Include(t => t.Exercicio)
        .Include(t => t.Treino)
        .Where(a => a.Treino.idTreino == id).ToListAsync();
    }
}