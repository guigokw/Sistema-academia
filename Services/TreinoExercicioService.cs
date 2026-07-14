public interface ITreinoExercicioService
{
    Treino obterTreino(int id);

    Exercicio obterExercicio(int id);

    TreinoExercicio obterTreinoExercicio(int idTreino, int idExercicio);

    void cadastrarTreinoExercicio(TreinoExercicio treinoExercicio);

    void deletarTreinoExercicio(int idTreino, int idExercicio);

    Task<IEnumerable<exibicaoTreinoExercicio>> listarExerciciosDoTreino(int id);
}

public class TreinoExercicioService : ITreinoExercicioService
{
    private readonly ITreinoExercicioRepository _treinoExercicio;

    public TreinoExercicioService(ITreinoExercicioRepository treinoExercicio)
    {
        _treinoExercicio = treinoExercicio;
    }

    public Treino obterTreino(int id)
    {
        Treino? treino = _treinoExercicio.acharTreino(id);

        if (treino == null)
        {
            throw new TreinoNaoEncontradoException("não foi possível adicionar exercicios aos treinos pois esse id não foi encontrado");
        }

        return treino;
    }

    public Exercicio obterExercicio(int id)
    {
        Exercicio? exercicio = _treinoExercicio.acharExercicio(id);

        if (exercicio == null)
        {
            throw new ExercicioNaoEncontradoException("não foi possível deletar o exercicio desse treino pois este não foi encontrado");
        }

        return exercicio;
    }

    public TreinoExercicio obterTreinoExercicio(int idTreino, int idExercicio)
    {
        TreinoExercicio? treinoExercicio = _treinoExercicio.acharTreinoExercicio(idTreino, idExercicio);

        if (treinoExercicio == null)
        {
            throw new TreinoExercicioNaoEncontradoException("não foi possível alterar o exercicio do treino pois este não foi encontrado");
        }

        return treinoExercicio;
    }

    public void cadastrarTreinoExercicio(TreinoExercicio treinoExercicio)
    {
        obterExercicio(treinoExercicio.IdExercicio);
        obterTreino(treinoExercicio.IdTreino);
        
        var treinoExercicioExiste = _treinoExercicio.acharTreinoExercicio(treinoExercicio.IdTreino, treinoExercicio.IdExercicio);

        if (treinoExercicioExiste != null)
        {
            throw new TreinoExercicioJaExisteExceptionException("não foi possível cadastrar esse exercicio nesse treino pois eles já estão relacionados");
        }

        _treinoExercicio.addTreinoExercicio(treinoExercicio);
        
    }

    public void deletarTreinoExercicio(int idTreino, int idExercicio)
    {
        
        TreinoExercicio treinoExercicio = obterTreinoExercicio(idTreino, idExercicio);
         _treinoExercicio.removeTreinoExercicio(treinoExercicio); 
    }


    public async Task<IEnumerable<exibicaoTreinoExercicio>> listarExerciciosDoTreino(int id)
    {
       var ExerciciosDoTreino = await _treinoExercicio.consultarExerciciosDoTreino(id);

       if (ExerciciosDoTreino == null || ! ExerciciosDoTreino.Any()) {
            throw new TreinoExercicioNaoEncontradoException("não foi possível listar os exercicios desse treino pois não há exercicios cadastrados nesse treino");
        }

        var exibirDados = ExerciciosDoTreino.Select(a => new exibicaoTreinoExercicio(
            a.Exercicio.IdExercicio,
            a.Exercicio.NomeExercicio,
            a.Exercicio.Series,
            a.Exercicio.Repeticoes,
            a.Exercicio.Descanso
        )).ToList();

        return exibirDados;
    }


}