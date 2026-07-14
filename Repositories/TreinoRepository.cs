public interface ITreinoRepository
{

    Treino? acharTreino(int id);
    void addTreino(Treino treino);

    void alterarNomeTreino(Treino treino, string nome);

    void alterarObjetivoTreino(Treino treino, string objetivo);

    void alterarDuracaoTreino(Treino treino, string duracao);

    void removeTreino(Treino treino);
}

public class TreinoRepository : ITreinoRepository
{
    private readonly AppDataContext _context;

    public TreinoRepository(AppDataContext context)
    {
        _context = context;
    }

    public Treino? acharTreino(int id)
    {
        return _context.Treinos.FirstOrDefault(a => a.idTreino == id);
    }

    public void addTreino(Treino treino)
    {
        _context.Treinos.Add(treino);
        _context.SaveChanges();
        
    }

    public void alterarNomeTreino(Treino treino, string nome)
    {
        treino.Nome = nome;
        _context.SaveChanges();
        
    }

    public void alterarObjetivoTreino(Treino treino, string objetivo)
    {
        treino.Objetivo = objetivo;
        _context.SaveChanges();
        
    }

    public void alterarDuracaoTreino(Treino treino, string duracao)
    {
        treino.duracaoTreino = duracao;
        _context.SaveChanges();
        
    }

    public void removeTreino(Treino treino)
    {
        _context.Remove(treino);
        _context.SaveChanges();
    }
}


