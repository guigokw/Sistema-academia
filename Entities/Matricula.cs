using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

public class Matricula
{

    private int _idMatricula;

    [Key]
    public int idMatricula
    {
        get {return _idMatricula;}
        set {
           
              if (value <= 0)
              {
                  throw new IdInvalidoException("não foi possível cadastrar essa matrícula pois o ID é inválido");
              }
              _idMatricula = value;
        }
    }

    private string cpfAluno = null!;

    public string CpfAluno
    {
        get { return cpfAluno; }
        set { 
             if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível cadastrar o aluno pois o CPF do aluno está nulo ou vazio");
            }
            cpfAluno = value; }
    }

    private Aluno aluno = null!;

    public Aluno Aluno
    {
        get { return aluno; }
        set { aluno = value; }
    }

    private int idPlano;

    public int IdPlano
    {
        get { return idPlano; }
        set { idPlano = value; }
    }

    private Plano plano = null!;

    public Plano Plano
    {
        get { return plano; }
        set { plano = value; }
    }

    private DateOnly dataInicio;

    public DateOnly DataInicio
    {
        get { return dataInicio; }
        set {
            
              if (value < DateOnly.FromDateTime(DateTime.Now))
                {
                   throw new DataInvalidaException("não foi possível cadastrar a matrícula pois a data de início é inválida");
                }
               dataInicio = value;
            
        }
    }

    private SituacaoPagamento _situacaoPagamento;

    public SituacaoPagamento SituacaoPagamento
    {
        get {return _situacaoPagamento;}

        set
        {
            _situacaoPagamento = value;
        }
    }

    private DateOnly dataVencimento;

    public DateOnly DataVencimento
    {
        get { return dataVencimento; }
        set {
            
            if (value < DateOnly.FromDateTime(DateTime.Now))
            {
                throw new DataInvalidaException("não foi possível cadastrar a matrícula pois a data de início é inválida");
            }
            dataVencimento = value;
            
        }
    }


    public Matricula()
    {
        
    }
}