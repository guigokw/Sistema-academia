using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

public class TreinoExercicio
{

    private int _idTreino;

    public int IdTreino
    {
        get {return _idTreino;}

        set
        {
            if (value < 0)
               {
                  throw new IdInvalidoException("não foi possível cadastrar o treino pois o id é inválido");
               }

            _idTreino = value;
        }
    }
    private Treino _treino = null!;

    public Treino Treino
    {
        get {return _treino;}

        set {_treino = value;}
    }
    
    private int _idExercicio;
    
    public int IdExercicio
    {
        get {return _idExercicio;}

        set
        {
            
            if (value < 0)
            {
                throw new IdInvalidoException("não foi possível adicionar o exercicio ao sistema pois o id está inválido");
            }

            _idExercicio = value;
            
        }
    }
    private Exercicio _exercicio = null!;

    public Exercicio Exercicio
    {
        get {return _exercicio;}

        set {_exercicio = value;}
    }

    public TreinoExercicio(Treino treino, Exercicio exercicio)
    {
        _treino = treino;
        _exercicio = exercicio;
    }

    public TreinoExercicio()
    {
        
    }
}