using Microsoft.Extensions.DependencyInjection;
using Sistema_academia;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Aluno
{


	private string? _nomeAluno;

	public string NomeAluno
	{
		get { return _nomeAluno!; }

		set { 
			if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível cadastrar o aluno pois o nome do aluno está nulo ou vazio");
            }
			_nomeAluno = value; }

	}

	
	private string? _cpfAluno;
    
	[Key]
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
                throw new StringInvalidaException("não foi possível cadastrar o aluno pois o CPF do aluno está nulo ou vazio");
            }

		     _cpfAluno = value;
			
		}
	}


    private string? _telefoneAluno;

    public string TelefoneAluno
	{
		get { return _telefoneAluno!;}

        set {
			

              bool numeroTelefone = Regex.IsMatch(value, @"^\d+$");


              if (value.Length != 11 || !numeroTelefone)
			  {
				  throw new TelefoneInvalidoException("não foi possível alterar o número de telefone pois o formato está invalido");
			  }

			  if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível inserir o telefone do aluno pois o valor inserido está nulo ou vazio");
            }
			
			  _telefoneAluno = value;
			
		
		}
    }

	private string? _emailAluno;

	public string EmailAluno
	{
		get { return _emailAluno!;}

		set
		{
			
			  if (!value.Contains("@"))
			  {
				  throw new EmailInvalidoException("não foi possível cadastrar esse email pois ele contém o formato inválido");
			  }

			  if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível cadastrar inserir o email do aluno pois o valor inserido está nulo ou vazio");
            }
			
			  _emailAluno = value;

			
				
		}
	}

	private DateOnly _dataNascimento;

	public string DataNascimento
	{
		get => _dataNascimento.ToString("dd/MM/yyyy");

		set
		{


			if (!DateOnly.TryParse(value, out DateOnly dataConvertida))
            {
              throw new FormatException("A data de nascimento inserida é inválida. Use o formato dd/MM/yyyy.");
            }

       
            DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);

       
           int idade = hoje.Year - dataConvertida.Year;

         
           if (dataConvertida > hoje.AddYears(-idade))
           {
              idade--;
           }

        
           if (idade < 18)
           {
              throw new IdadeInvalidaException("Não foi possível cadastrar o aluno pois a idade dele é menor do que 18.");
           }

		   if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível inserir a data de nascimento do aluno pois o valor inserido está nulo ou vazio");
            }

        
          _dataNascimento = dataConvertida;
    
			
		}
	}

	private Boolean _ativo;

	public Boolean Ativo
	{
		get { return _ativo; }
		set { _ativo = value; }
	}

    
	public Aluno () {

	}

}
	


