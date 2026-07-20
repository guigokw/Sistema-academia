public interface IPlanoService
{
    Plano obterPlano(int id);

    void cadastrarPlano(Plano plano);

    void removerPlano(int id);

    void atualizarPreco(int id, decimal novoPreco);

    Task<List<ExibicaoPlanosDTO>> listarPlanos();

    Task<TiposDePlano> listarPlanoMaisContratado();
}

public class PlanoService : IPlanoService
{
    private readonly IPlanoRepository _planoRepository;

    public PlanoService(IPlanoRepository planoRepository)
    {
        _planoRepository = planoRepository;
    }

    public Plano obterPlano(int id)
    {
        Plano? plano = _planoRepository.acharPlano(id);

        if (plano == null)
        {
            throw new PlanoNaoEncontradoException("não foi possível realizar a operação pois o plano não foi encontrado.");
        }
        return plano;
    }

    public void cadastrarPlano(Plano plano)
    {
        var planoExistente = _planoRepository.acharPlano(plano.IdPlano);

        if (planoExistente != null)
        {
            throw new PlanoJaExisteException("não foi possível cadastrar o plano pois um plano com esse id já está cadastrado no sistema.");
        }

        _planoRepository.addPlano(plano);
    }

    public void removerPlano(int id)
    {
        Plano plano = obterPlano(id);
        _planoRepository.removePlano(plano);
    }

    public void atualizarPreco(int id, decimal novoPreco)
    {
        Plano plano = obterPlano(id);

        if (novoPreco <= 0)
        {
            throw new ValorInvalidoException("não foi possível atualizar o preço do plano pois o valor informado é menor ou igual a zero.");
        }

        _planoRepository.atualizarPreco(plano, novoPreco);
    }

    public async Task<List<ExibicaoPlanosDTO>> listarPlanos()
    {
        var planos = await _planoRepository.listarPlanos();

        if (planos == null || !planos.Any())
        {
            throw new PlanoNaoEncontradoException("não foi possível listar os planos pois não há nenhum plano cadastrado no sistema.");
        }

        var exibirDados = planos.Select(p => new ExibicaoPlanosDTO
        (p.IdPlano,
        p.NomePlano,
        p.TipoPlano,
        p.ValorPlano))
        .ToList();

        return exibirDados;
    }

    public async Task<TiposDePlano> listarPlanoMaisContratado()
    {
         return await _planoRepository.planoMaisContratado();
    }
}