using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

public class Exercicio
{
    private int _idExercicio;

    [Key]
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

    private string? _nomeExercicio;
    public string NomeExercicio
    {
        get { return _nomeExercicio!; }

        set {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível cadastrar o exercício pois o nome do exercício está nulo ou vazio");
            }
             _nomeExercicio = value; }
    }

    private int _series;

    public int Series
    {
        get {return _series;}

        set
        {
            
              if (value < 1 || value > 7)
              {
                  throw new SeriesInvalidaException("não foi possível alterar o número de series pois ela é inválida necessitando ser entre 1 e 7");
              }

              _series = value;
            
         }
    }

    private int _repeticoes;

    public int Repeticoes
    {
        get {return _repeticoes;}

        set
        {
            
              if (value < 8 || value > 12)
              {
                  throw new RepeticoesInvalidaException("não foi possível alterar as repetições desse exercicios pois é necessários estar entre 8 a 12 repetições");
              }
              _repeticoes = value;
            
        }
    }

    private int _descanso;

    public int Descanso
    {
        get {return _descanso;}

        set
        {
          
              if (value < 1 || value > 10)
              {
                  throw new TempoDescansoInvalidoException("não foi possível alterar o tempo de descanso do exercicio pois tem que ser entre 1 a 10 minutos de descanso");
              }

              _descanso = value;
            
        }
    }

    public Exercicio()
    {
        
    }
}