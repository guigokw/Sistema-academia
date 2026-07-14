using Microsoft.EntityFrameworkCore;

public interface IPlanoRepository
{
    Plano? acharPlano(int id);

    void addPlano(Plano plano);

    void removePlano(Plano plano);

    void atualizarPreco(Plano plano, decimal novoPreco);

    Task<List<Plano>> listarPlanos();
}

public class PlanoRepository : IPlanoRepository
{
    private readonly AppDataContext _context;
    public PlanoRepository(AppDataContext context)
    {
        _context = context;
    }

    public Plano? acharPlano(int id)
    {
        return _context.Planos.FirstOrDefault(p => p.IdPlano == id);
    }

    public void addPlano(Plano plano)
    {
        _context.Planos.Add(plano);
        _context.SaveChanges();
    }

    public void removePlano(Plano plano)
    {
        _context.Planos.Remove(plano);
        _context.SaveChanges();
    }

    public void atualizarPreco(Plano plano, decimal novoPreco)
    {
        plano.ValorPlano = novoPreco;
        _context.SaveChanges();
    }

    public async Task<List<Plano>> listarPlanos()
    {
        return await _context.Planos.ToListAsync();
    }
}