using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client.Extensibility;

public class Treino
{
    private int _idTreino;

    [Key]
    public int idTreino
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

    private string? _nome;

    public string Nome {
        get {return _nome!;}

        set {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível cadastrar o treino pois o nome do treino está nulo ou vazio");
            }
            _nome = value;
        }
    }

    private string? _objetivo;

    public string Objetivo
    {
        get {return _objetivo!;}

        set {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível cadastrar o treino pois o objetivo do treino está nulo ou vazio");
            }
            _objetivo = value;
        }
    }

    private TimeOnly _duracao_treino;

    public string duracaoTreino
    {
        get { return _duracao_treino.ToString("HH:mm"); }

        set
        {
            if (!TimeOnly.TryParse(value, out TimeOnly duracao))
            {
                throw new DuracaoTreinoInvalidaException("não foi possível cadastrar o treino pois a duração do treino está em um formato inválido, por favor insira no formato HH:mm");
            }

            _duracao_treino = duracao;
    
        }
    }

    public Treino()
    {
        
    }
}