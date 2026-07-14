using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Plano
{
    private int idPlano;

    [Key]
    public int IdPlano
    {
        get { return idPlano; }
        set {
           
            if (value <= 0)
            {
                throw new IdInvalidoException("não foi possível cadastrar o plano pois o id do plano é inválido");
            }
            idPlano = value;
            
        }
    }

    private string? nomePlano;

    public string NomePlano
    {
        get { return nomePlano!; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new StringInvalidaException("não foi possível cadastrar o plano pois o nome do plano está nulo ou vazio");
            }

            nomePlano = value;
        }
    }

    private TiposDePlano tipoPlano;

    public TiposDePlano TipoPlano
    {
        get { return tipoPlano; }
        set { tipoPlano = value; }
    }

    private decimal valorPlano;

    [Column(TypeName = "decimal(18,2)")]
    public decimal ValorPlano
    {
        get { return valorPlano; }

        set {
            
            if (value <= 0)
            {
                throw new ValorInvalidoException("não foi possível cadastrar o plano pois o valor do plano é inválido");
            }
            valorPlano = value;
            
        }
    }

    public Plano()
    {
        
    }

}