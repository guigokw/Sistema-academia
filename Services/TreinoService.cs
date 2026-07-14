public interface ITreinoService
{
    Treino ObterTreino(int id);

    void cadastrarTreino(Treino treino);

    void altNomeTreino(int idTreino, string nomeTreino);

    void altObjetivoTreino(int idTreino, string objetivoTreino);

    void altDuracaoTreino(int idTreino, string duracaoTreino);

    void deletarTreino(int id);
}

public class TreinoService : ITreinoService
{

    private readonly ITreinoRepository _treinoRepository;

    public TreinoService(ITreinoRepository treinoRepository)
    {
        _treinoRepository = treinoRepository;
    }
    public Treino ObterTreino(int id)
    {
        Treino? treino = _treinoRepository.acharTreino(id);

        if (treino == null ) 
        {
           throw new TreinoNaoEncontradoException("não foi possível encontrar o treino pois ele não está registrado no sistema");
        }
        return treino;
    }

    public void cadastrarTreino(Treino treino)
    {
        
        var treinoExiste = _treinoRepository.acharTreino(treino.idTreino);

        if (treinoExiste != null)
        {
            throw new TreinoRepetidoException("não foi possível cadastrar esse treino pois seu id já consta no sistema");
        }

        _treinoRepository.addTreino(treino);
        
    }

    public void altNomeTreino(int idTreino, string nomeTreino)
    {
        
        Treino treino = ObterTreino(idTreino);

        _treinoRepository.alterarNomeTreino(treino, nomeTreino);
        
    }

    public void altObjetivoTreino(int idTreino, string objetivoTreino)
    {
        
        Treino treino = ObterTreino(idTreino);
        _treinoRepository.alterarObjetivoTreino(treino, objetivoTreino);
        
    }

    public void altDuracaoTreino(int idTreino, string duracaoTreino)
    {
        
        
        Treino treino = ObterTreino(idTreino);
        _treinoRepository.alterarDuracaoTreino(treino, duracaoTreino);
       
    }

    public void deletarTreino(int id)
    {
       
        Treino treino = ObterTreino(id);
        _treinoRepository.removeTreino(treino);
        
    }
}