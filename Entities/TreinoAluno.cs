

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class TreinoAluno
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

    private string? _cpfAluno;

    public string CpfAluno
    {
        get {return _cpfAluno!;}

        set {
            if (value.Length != 11)
			  {
				 throw new CpfInvalidoException("não foi possível cadastrar o CPF do aluno pois ele não tem 11 caracteres");
              }

			  bool temApenasNumeros = Regex.IsMatch(value, @"^\d+$");

              if (! temApenasNumeros) 
			  {
				 throw new CpfInvalidoException("não foi possível cadastrar o CPF do aluno pois ele contém caracteres inválidos");
			  }

              if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível cadastrar o CPF do aluno pois ele está nulo ou vazio");
            }
            
            _cpfAluno = value;
            }
    }

    private Aluno _aluno = null!;

    public Aluno Aluno
    {
        get {return _aluno;}

        set {_aluno = value;}
    }

    
    public TreinoAluno()
    {
        
    }
}