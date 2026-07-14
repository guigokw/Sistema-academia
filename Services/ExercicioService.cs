public interface IExercicioService
{
    Exercicio obterExercicio(int id);

    void cadastrarExercicio(Exercicio exercicio);

    void altSeries(int id, int series);

    void altRepeticoes(int id, int repeticoes);

    void altDescanso(int id, int descanso);

    void excluirExercicio(int id);
}

public class ExercicioService : IExercicioService
{
    private readonly IExercicioRepository _exercicioRepository;
    
    public ExercicioService(IExercicioRepository exercicioRepository)
    {
        _exercicioRepository = exercicioRepository;
    }

    public Exercicio obterExercicio(int id)
    {
        Exercicio? exercicio = _exercicioRepository.acharExercicio(id);

        if (exercicio == null)
        {
            throw new ExercicioNaoEncontradoException("não foi possível realizar a operação pois esse exercicio não foi encontrado");
        }

        return exercicio;
    }

    public void cadastrarExercicio(Exercicio exercicio)
    {
       
          var exercicioExiste = _exercicioRepository.acharExercicio(exercicio.IdExercicio);

          if (exercicioExiste != null)
          {
              throw new ExercicioJaExisteException("não foi possível cadastrar esse exercicio pois seu identificador já está registrado no sistema, por favor defina outro");
          }

          _exercicioRepository.addExercicio(exercicio);

    }

    public void altSeries(int id, int series)
    {
    
        Exercicio exercicio = obterExercicio(id);
        _exercicioRepository.alterarSeries(exercicio, series);
        
    }

    public void altRepeticoes(int id, int repeticoes)
    {
        
        Exercicio exercicio = obterExercicio(id);
        _exercicioRepository.alterarRepeticoes(exercicio, repeticoes);
        
    }

    public void altDescanso(int id, int descanso)
    {
        
        Exercicio exercicio = obterExercicio(id);
        _exercicioRepository.alterarDescanso(exercicio, descanso);
        
    }

    public void excluirExercicio(int id)
    {

        Exercicio exercicio = obterExercicio(id);
        _exercicioRepository.removeExercicio(exercicio);
        
    }
}