using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;

public class Pagamento
{
    private int _idPagamento;

    [Key]
    public int IdPagamento
    {
        get {return _idPagamento;}

        set
        {
            
            if (value < 0) {
                throw new IdInvalidoException("não foi possível registrar o pagamento pois o identificador é inválido");
            }

            _idPagamento = value;
           
        }
    }

    private decimal _valorPagamento;

    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorPagamento
    {
        get {return _valorPagamento;}

        set
        {
            _valorPagamento = value;
            
        }
    }

    private DateOnly? _dataPagamento;

    public DateOnly? DataPagamento
    {
        get {return _dataPagamento;}

        set
        {  
            _dataPagamento = value;  
        }
    }

    public int MesReferencia { get; set; }

    public int AnoReferencia { get; set; }


    private int _idMatricula;

    public int IdMatricula
    {
        get {return _idMatricula;}

        set
        {
            if (value < 0)
            {
                throw new IdInvalidoException("não foi possível realizar o pagamento pois o id da matrícula é inválido");
            }

            _idMatricula = value;
        }
    }

    private Matricula _matricula = null!;

    public Matricula Matricula
    {
        get {return _matricula;}

        set
        {
            _matricula = value;
        }
    }

    public Pagamento()
    {
        
    }


}