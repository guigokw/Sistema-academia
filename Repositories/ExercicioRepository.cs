public interface IExercicioRepository
{
    Exercicio? acharExercicio(int id);

    void addExercicio(Exercicio exercicio);

    void alterarSeries(Exercicio exercicio, int series);

    void alterarRepeticoes(Exercicio exercicio, int repeticoes);

    void alterarDescanso(Exercicio exercicio, int descanso);

    void removeExercicio(Exercicio exercicio);
}

public class ExercicioRepository : IExercicioRepository
{
    private readonly AppDataContext _context;

    public ExercicioRepository(AppDataContext context)
    {
        _context = context;
    }

    public Exercicio? acharExercicio(int id)
    {
        return _context.Exercicios.FirstOrDefault(a => a.IdExercicio == id);
    }

    public void addExercicio(Exercicio exercicio)
    {
        _context.Exercicios.Add(exercicio);
        _context.SaveChanges();
        
    }

    public void alterarSeries(Exercicio exercicio, int series)
    {
        exercicio.Series = series;
        _context.SaveChanges();

    }

    public void alterarRepeticoes(Exercicio exercicio, int repeticoes)
    {
        exercicio.Repeticoes = repeticoes;
        _context.SaveChanges();
       
    }

    public void alterarDescanso (Exercicio exercicio, int descanso)
    {
        exercicio.Descanso = descanso;
        _context.SaveChanges();
        
    }

    public void removeExercicio(Exercicio exercicio)
    {
        _context.Exercicios.Remove(exercicio);
        _context.SaveChanges();
        
    }
}